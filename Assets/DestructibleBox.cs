using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour
{
    public GameObject itself;
    public GameObject explosion;
    public float HP = 10.0f;
    private Color originalColor;



    // Start is called before the first frame update
    void Start()
    {
        if (explosion == null){
            explosion = GameObject.Find("Explosion");
        }
        originalColor = itself.GetComponentInChildren<Renderer>().material.color;
        // if(audioSource == null){
        //     audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        // }
    }
    void Flash() {
      //visor.SetActive(false);
      itself.GetComponentInChildren<Renderer> ().material.color = Color.red;
      StartCoroutine("SomeFunctionAfterSomeTime");
    }

    IEnumerator SomeFunctionAfterSomeTime()
    {
        yield return new WaitForSeconds(0.05F);
        itself.SetActive(true);
        //Debug.Log("x");
        itself.GetComponentInChildren<Renderer>().material.color = originalColor;
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
        //Debug.Log(HP);
       
    }

}
