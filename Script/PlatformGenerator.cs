using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject healthUp;
    [SerializeField] private GameObject jumpUp;
    [SerializeField] private GameObject slowDown;
    [SerializeField] private GameObject wall;
    [SerializeField] private float distanceThreshold;
    GameObject player;
    private Vector3 nextPlatformPos = Vector3.zero;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(platform, nextPlatformPos, Quaternion.identity);
        nextPlatformPos += new Vector3(0, 0, 55);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(nextPlatformPos, player.transform.position) < distanceThreshold){
            GameObject plat = Instantiate(platform, nextPlatformPos, Quaternion.identity);
            plat.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
            for(int i = 0; i < 3; i++){
                obstacle.transform.position = new Vector3(Random.Range(-2, 3), 1, Random.Range(-20, 21));
                wall.transform.position = new Vector3(Random.Range(-2,3), 1, Random.Range(-20, 21));
                GameObject obs = Instantiate(obstacle, nextPlatformPos + obstacle.transform.position, Quaternion.identity);
                GameObject w = Instantiate(wall, nextPlatformPos + wall.transform.position, Quaternion.identity);
                obs.transform.parent = plat.transform;
                w.transform.parent = plat.transform;
            }
            int num = Random.Range(1, 4);
            if(num == 1){
                healthUp.transform.position = new Vector3(Random.Range(-2, 3), 2, Random.Range(-20, 21));
                GameObject hu = Instantiate(healthUp, nextPlatformPos + healthUp.transform.position, Quaternion.identity);
                hu.transform.parent = plat.transform;
            }
            else if (num == 2){
                jumpUp.transform.position = new Vector3(Random.Range(-2, 3), 2, Random.Range(-20, 21));
                GameObject ju = Instantiate(jumpUp, nextPlatformPos + jumpUp.transform.position, Quaternion.identity);
                ju.transform.parent = plat.transform;
            }
            else if(num ==3){
                slowDown.transform.position = new Vector3(Random.Range(-2, 3), 2, Random.Range(-20, 21));
                GameObject sd = Instantiate(slowDown, nextPlatformPos + slowDown.transform.position, Quaternion.identity);
                sd.transform.parent = plat.transform;
            }
            

            nextPlatformPos += new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 55);
        }  
    }
}
