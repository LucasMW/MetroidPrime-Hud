using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public float reloadTime;
    // Start is called before the first frame update
    void Start()
    {
        if(reloadTime == null){
        	reloadTime = 1;
        }
        Reload();
    }

    void Reload() {
      StartCoroutine("SomeFunctionAfterSomeTime");
    }

    IEnumerator SomeFunctionAfterSomeTime()
    {
        yield return new WaitForSeconds(1F);
       	SceneManager.LoadScene("MixAndJam");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
