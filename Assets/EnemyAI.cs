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
    private float coolDown = 0.2f;
    private bool shouldShoot = false;
    private float shotsLeft = 3;

    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip noticeSound;

    // Start is called before the first frame update
    void Start()
    {
        seesYou = false;
        lightRef.GetComponent<Renderer> ().material.color = Color.green;
        if(audioSource == null){
            audioSource = GetComponent<AudioSource>();
            Debug.Log(audioSource);
        }
        
        //awereness = 30;
        //cannonParticleShooter = itself.GetComponentInChildren<ParticleSystem>();
    }

    void Shoot() {
        //Debug.Log("Shoot");
        cannonParticleShooter.Play();
        audioSource.PlayOneShot(shootSound, 0.1F);

    }

    void LateUpdate()
    {
        // ones per in seconds
        if(seesYou){
            TimeInterval += Time.deltaTime;
            coolDown -= Time.deltaTime;

            if (TimeInterval >= 0.3 && shotsLeft > 0 && coolDown <= 0)
            {
                Shoot();
                shotsLeft--;
                coolDown = 0.1f;
            }
            if (TimeInterval >= 1)
            {
                TimeInterval = 0;
                shotsLeft = 3;
                coolDown = 0.2f;
            
            }
        }  else {
            TimeInterval = 0;
            shotsLeft = 3;
            coolDown =  0.2f;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(itself.transform.position, playerRef.transform.position);
        if(dist < awereness) {
            seesYou = true;
            lightRef.GetComponent<Renderer> ().material.color = Color.red;
            //audioSource.PlayOneShot(noticeSound, 0.01F);

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
