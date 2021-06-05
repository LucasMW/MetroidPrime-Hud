using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	bool isOpen = false;
	bool shouldOpen() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		return enemies.Length <= 0;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldOpen()){
        	isOpen = true;
        }
        if(isOpen){
        	this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
