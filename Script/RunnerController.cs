using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{

    [SerializeField] private GameController gameController;
    [SerializeField] private bool isGrounded = true;
    GameObject player;

    private void Awake(){
        //Debug.Log("Awake - Time - " + Time.time);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Start is called before the first frame update
    void Start(){
        //Debug.Log("Start - Time - " + Time.time);
        if(!gameController){
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("Update - Time - " + Time.deltaTime);
        //Debug.Log("Update - Time - " + gameObject.transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gameController.jumpIntensity, ForceMode.Impulse);
        if(!isGrounded && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gameController.jumpIntensity, ForceMode.Impulse);
        if(player.transform.position.y < -5)
            gameController.health = 0;
        
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Time.deltaTime * gameController.speed));
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Platform")
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Platform")
            isGrounded = false;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Wall"){
            Debug.Log("Obstacle Hit!");
            gameController.health -= 10;
        }

        if(other.gameObject.tag == "healthUp"){
            //Debug.Log("Health Up Hit!");
            gameController.health = 50;
        }

        if(other.gameObject.tag == "jumpUp"){
            //Debug.Log("Health Up Hit!");
            gameController.jumpIntensity += 1;
        }

        if(other.gameObject.tag == "slowDown"){
            //Debug.Log("Health Up Hit!");
            gameController.speed -= 1;
        }
        
    }

    void FixedUpdate(){
        //Debug.Log("Fixed Update - Time - " + Time.deltaTime);
    }
}
