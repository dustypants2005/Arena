using System.Collections;
using System.Collections.Generic;
using dustypants.Environment;
using UnityEngine;

namespace dustypants.Utility {
  public class Spawner : MonoBehaviour {
    public GameObject GO;
    private bool isSpawning = false;
    public void Spawn(float t){
      if(!isSpawning)
        StartCoroutine(Respawn(t));
      isSpawning = true;
    }

    IEnumerator Respawn(float t){
      yield return new WaitForSeconds(t);
      if(isSpawning){
          var fp = Instantiate(GO, transform).GetComponent<FallingPlatform>();
          fp.spawner = this;
          isSpawning = false;
        }
    }
  }
}