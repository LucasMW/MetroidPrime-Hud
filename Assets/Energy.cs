using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
	public float recoverHP = 10;

	private GameObject player;
	private ArmCannon cannon;

	public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FPSController");
        cannon = player.GetComponent<ArmCannon>();
    }

    void FixedUpdate() {
    	if(cannon.charging){
    		Vector3 direction = player.transform.position - transform.position;

            float distance = direction.magnitude;

            direction = direction.normalized;

            Debug.Log(gameObject.GetComponent<Rigidbody>().velocity );

            //gameObject.GetComponent<Rigidbody>().velocity = (direction * 100 / (distance+1));

            gameObject.GetComponent<Rigidbody>().AddForce(direction * 200 / (distance*distance+1));

    	} else {
    		gameObject.GetComponent<Rigidbody>().AddForce(gameObject.GetComponent<Rigidbody>().velocity * -10.0F);
    	}
    	
    }

    // Update is called once per frame
    // void OnCollisionEnter(Collision collision)
    // {
    // 	string tag  = collision.gameObject.tag;
    // 	Debug.LogWarning(tag);
    // 	if(tag == "Player")
    // 	{
    //     	PlayerHealth hp = collision.gameObject.GetComponent<PlayerHealth>();
    //     	hp.life += recoverHP;

    // 	}
    // }
    private void OnTriggerEnter(Collider other)
    {
    	string tag  = other.gameObject.tag;
    	//Debug.LogWarning(tag);
    	if(tag == "Player")
    	{
        	PlayerHealth hp = other.gameObject.GetComponent<PlayerHealth>();
        	hp.Recover(recoverHP);
        	Instantiate(particleEffect, other.gameObject.transform.position,  Quaternion.Euler(-90 , 0, 0));
        	Destroy(gameObject);
    	}
    }
}
