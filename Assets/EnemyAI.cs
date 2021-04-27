using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public GameObject itself;
    public GameObject lightRef;
	public GameObject playerRef;
    public float awereness;
	private bool seesYou; 
    public ParticleSystem cannonParticleShooter;

    private float TimeInterval ;

    // Start is called before the first frame update
    void Start()
    {
        seesYou = false;
        lightRef.GetComponent<Renderer> ().material.color = Color.green;
        //awereness = 30;
        //cannonParticleShooter = itself.GetComponentInChildren<ParticleSystem>();
    }

    void Shoot() {
        Debug.Log("Shoot");
        cannonParticleShooter.Play();
    }

    void LateUpdate()
    {
        // ones per in seconds
        if(seesYou){
            TimeInterval += Time.deltaTime;

            if (TimeInterval >= 0.3)
            {
                Shoot();
            }
            if (TimeInterval >= 1)
            {
                TimeInterval = 0;
            
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(itself.transform.position, playerRef.transform.position);
        if(dist < awereness) {
            seesYou = true;
            lightRef.GetComponent<Renderer> ().material.color = Color.red;

        } else {
            seesYou = false;
            lightRef.GetComponent<Renderer> ().material.color = Color.green;
        }

        if(seesYou) {
            var mem = itself.transform.rotation;
            Vector3 target = new Vector3(playerRef.transform.position.x,itself.transform.position.y, playerRef.transform.position.z);
            itself.transform.LookAt(target);
            //Shoot();
        }
        
        
    }
}
