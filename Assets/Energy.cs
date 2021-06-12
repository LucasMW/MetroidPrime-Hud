using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
	public float recoverHP = 10;

	private GameObject player;
	private ArmCannon cannon;
    private Rigidbody rigidbody;

	public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player") ?? GameObject.FindWithTag("Player");
        cannon = player.GetComponent<ArmCannon>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine("Byebye");
 	}
    
    IEnumerator Byebye()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void FixedUpdate() {
    	if(cannon.charging){
    		Vector3 direction = player.transform.position - transform.position;
            float distance = direction.magnitude; 

            if(distance > 20) {
            	return;
            }

            direction = direction.normalized;
            rigidbody.AddForce(direction * 200 / (distance*distance+1));

    	} else {
    		rigidbody.AddForce(rigidbody.velocity * -10.0F);
    		if(rigidbody.velocity.magnitude < 0.01) {
    			rigidbody.velocity = Vector3.zero; 
    		}
    	}
    	
    }

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
