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
    public GameObject visor;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        visor = GameObject.Find("vidro");
        Debug.Log(visor);
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

    void Flash() {
      //visor.SetActive(false);
      visor.GetComponentInChildren<Renderer> ().material.color = Color.red;
      StartCoroutine("SomeFunctionAfterSomeTime");
    }

    IEnumerator SomeFunctionAfterSomeTime()
    {
        yield return new WaitForSeconds(0.1F);
        visor.SetActive(true);
        //Debug.Log("x");
        visor.GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 20), "Shield: " + life);
    }   
    void GameOver() {
    	Application.LoadLevel(Application.loadedLevel);
    	//SceneManager.LoadScene("GameOver");
    }
    void OnParticleCollision(GameObject other)
    {

        //Debug.Log(other.name);
        if(other.name == "NormalBeamParticle") {
            life -= 1.0f;
        } else if(other.name == "ChargedBeamParticle"){
            life -= 70.0f;
        }
        else if(other.name == "EnemyBeamParticle"){
            life -= 2.0f;
        }
        else {
        	life -=5.0f;
        }
        audioSource.PlayOneShot(damageAudio, 0.3F);
        Flash();

        
        //Perhaps play flashing animation?
        //Debug.Log(life);
       
    }
}
