using UnityEngine;
using System.Collections;

public class FlashObject : MonoBehaviour {

 // vars to Flash a colour when hit 
 public Color NormalColour = Color.white;
 public Color FlashColour = Color.red;

 public Renderer GameMesh;
 public float FlashDelay = 0.025f;
 public int TimesToFlash = 3;

 public void Flash(){

  StartCoroutine(OnCollisionEnter());
 }

 private IEnumerator  OnCollisionEnter() {
  var renderer = GameMesh; 
  if (renderer != null) {  

   for (int i = 1; i <= TimesToFlash; i++) {
    renderer.material.color = FlashColour; 
    yield return new WaitForSeconds (FlashDelay);
    renderer.material.color = NormalColour;
    yield return new WaitForSeconds (FlashDelay);
   }
  }
 }

}