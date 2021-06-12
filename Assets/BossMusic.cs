using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
	public AudioClip bossMusic;
	public AudioClip normalMusic;
	public GameObject boss;
	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = normalMusic;
        audioSource.Play();
    }
    bool isBossPresent() {
    	return boss != null || GameObject.FindGameObjectWithTag("Boss") != null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBossPresent()) {
        	if(audioSource.clip != bossMusic) {
        		audioSource.clip = bossMusic;
        		audioSource.Play();
        	}
        } else {
        	if(audioSource.clip != normalMusic){
        		audioSource.clip = normalMusic;
        		audioSource.pitch = 1;
        		gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
        		audioSource.Play();
        	}
        }
    }
}
