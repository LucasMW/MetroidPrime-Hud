using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissleAmmo : MonoBehaviour
{
    public float ammo;
    public float maxAmmo;
	public GameObject itself;
    public AudioClip fireShotAudio;
    public AudioSource audioSource;
    public GameObject visor;
    public GameObject missleNumberText;
    public GameObject missleBar;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = 50;
        ammo = maxAmmo;
        visor = GameObject.Find("vidro");
        Debug.Log(visor);
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo <= 0){
        	Debug.Log("Depleted Missles");
        }
        missleNumberText.GetComponent<Text>().text =  ammo < 10 ? "0"+ ammo : ammo + "";
        missleBar.GetComponent<RectTransform>().localScale = new Vector3(1f, 1.0F*ammo/maxAmmo, 1f);
    }

    public void Recover(float recovered){
        ammo += recovered;
        ammo = ammo > maxAmmo ? maxAmmo : ammo;
    }


    void OnGUI() {
        GUI.Label(new Rect(100, 10, 100, 20), "Ammo: " + ammo);
    }   
    
}