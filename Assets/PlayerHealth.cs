using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public float life;
	public GameObject itself;
    public AudioClip damageAudio;
    public AudioSource audioSource;
    public GameObject visor;
    public GameObject lifeNumberText;
    public GameObject lifeBar;
    public GameObject energyTankGroup;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        visor = GameObject.Find("vidro");
        Debug.Log(visor);
    }
    void setEnergyTanks(int amount) {
        Transform[] allChildren  = energyTankGroup.GetComponentsInChildren<Transform>();

        Debug.Log("allChildren " + allChildren.Length);
        foreach (Transform child in allChildren)
        {
            Image img = child.gameObject.GetComponent<Image>();
            Debug.Log(child.gameObject);
            Debug.Log(img);
            if(img != null){
                img.enabled = false;
            }
            //img.enabled = false;
            //child.gameObject.SetActive(false);
        }
        for(int i = 0; i < amount+1; i++){
           Image img = allChildren[i].gameObject.GetComponent<Image>();
           if(img != null){
                img.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0){
        	Debug.Log("Game Over");
        	GameOver();
        	//Destroy(itself);
        }
        int lifeNumber = ((int)life) % 100;
        lifeNumberText.GetComponent<Text>().text =  lifeNumber < 10 ? "0"+ lifeNumber : lifeNumber + "";
        Debug.Log(lifeNumber);
        int energyTankSquares = ((int)life) / 100;
        Debug.Log(energyTankSquares);
        setEnergyTanks(energyTankSquares);
        lifeBar.GetComponent<RectTransform>().localScale = new Vector3(1.0F*lifeNumber/99.0F, 1f, 1f);
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
