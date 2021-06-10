using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float destroyTimeSeconds;
    // Start is called before the first frame update
    void Start()
    {
    	if(destroyTimeSeconds == null || destroyTimeSeconds <= 2){
    		destroyTimeSeconds = 2;
    	}
        StartCoroutine("Byebye");
    }


    IEnumerator Byebye()
    {
        yield return new WaitForSeconds(destroyTimeSeconds);
        //Debug.Log("x");
        Destroy(gameObject);
    }
}
