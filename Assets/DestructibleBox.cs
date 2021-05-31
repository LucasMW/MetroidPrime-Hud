﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour
{
    public GameObject itself;
    public GameObject explosion;
    public float HP = 10.0f;



    // Start is called before the first frame update
    void Start()
    {
        if (explosion == null){
            explosion = GameObject.Find("Explosion");
        }
        // if(audioSource == null){
        //     audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        // }
    }
    void Flash() {
      Renderer renderer = GetComponentInChildren<Renderer>();
      Material mat = renderer.material;
     
      float emission = Mathf.PingPong(Time.time, 1.0f);
      Color baseColor = renderer.material.color; //Replace this with whatever you want for your base color at emission level '1'
     
      Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
 
      mat.SetColor("_EmissionColor", finalColor);
    }

    void Explode() {
            float x = itself.transform.position.x;
            float y = itself.transform.position.y;
            float z = itself.transform.position.z;

            Instantiate(explosion, new Vector3(x,y-1,z),  Quaternion.Euler(-90 , 0, 0));
            Destroy(itself);
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0){
            Debug.Log("Should Destroy");
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision);
    }
    void OnParticleCollision(GameObject other)
    {

        //Debug.Log(other.name);
        if(other.name == "NormalBeamParticle") {
            HP -= 10.0f;
        } else if(other.name == "ChargedBeamParticle"){
            HP -= 70.0f;
        } else if(other.name == "EnemyBeamParticle"){
            HP -= 5.0f;
        }
        Flash();
        //Perhaps play flashing animation?
        Debug.Log(HP);
       
    }

}
