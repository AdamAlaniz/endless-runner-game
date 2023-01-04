using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int health = 50;
    public int speed = 10;
    public float jumpIntensity = 7f;
    public int timer = 10;
    [SerializeField] private Text timerText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level: " + SceneManager.GetActiveScene().name;
        health = PlayerPrefs.GetInt("PlayerHealth", 50);
        speed = PlayerPrefs.GetInt("PlayerSpeed", 10);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown(){
        while(timer > 0){
            timerText.text = "Timer: " + timer;
            //wait one second
            yield return new WaitForSeconds(1);
            timer--;
        }
        // pause the player, load the UI message
        GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerController>().enabled = false;
        yield return new WaitForSeconds(2);
        ChangeLevel();

    }

    private void ChangeLevel(){
        PlayerPrefs.SetInt("PlayerHealth", health - 10);
        PlayerPrefs.SetInt("PlayerSpeed", speed + 5);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
    }
    
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = new Vector3(health / 50f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if(health <= 0)
            GameOver();
    }

    public void PauseGame(){
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerController>().enabled = false;
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<RunnerController>().enabled = true;
    }

    public void GameOver(){
        Debug.Log("GAME OVER!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnApplicationQuit(){
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
    }
}
