using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public float life;
	public GameObject itself;
    public AudioClip damageAudio;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0){
        	Debug.Log("Game Over");
        	GameOver();
        	//Destroy(itself);
        }
    }
    void GameOver() {
    	Application.LoadLevel(Application.loadedLevel);
    	//SceneManager.LoadScene("GameOver");
    }
    void OnParticleCollision(GameObject other)
    {

        //Debug.Log(other.name);
        if(other.name == "NormalBeamParticle") {
            life -= 10.0f;
        } else if(other.name == "ChargedBeamParticle"){
            life -= 70.0f;
        }
        else if(other.name == "EnemylBeamParticle"){
            life -= 1.0f;
        }
        else {
        	life -=5.0f;
        }
        audioSource.PlayOneShot(damageAudio, 0.1F);
        
        //Perhaps play flashing animation?
        Debug.Log(life);
       
    }
}
