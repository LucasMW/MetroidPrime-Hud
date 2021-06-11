using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){

        	if(isPaused){
        		ResumeGame();
        	} else {
        		PauseGame();
        	}
        	isPaused = !isPaused;
        } 
    }
    void PauseGame ()
    {
        Time.timeScale = 0;
    }

	void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
