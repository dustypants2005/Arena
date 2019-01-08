using System.Collections;
using System.Collections.Generic;
using dustypants.Utility;
using UnityEngine;

namespace dustypants.Environment {
  [RequireComponent(typeof(Rigidbody))]
  public class FallingPlatform : MonoBehaviour {
    public float FallDelay = 1;
    public float DestroyDelay = 1;
    public Spawner spawner;
    public float respawnTime = 1;
    private Rigidbody rb;

    private void Start() {
      rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        StartCoroutine(Drop());
      }
    }

    IEnumerator Drop(){
      // TODO: play animation
      yield return new WaitForSeconds(FallDelay);
      if(spawner != null){
        spawner.Spawn(respawnTime);
      }
      rb.useGravity = true;
      Destroy(gameObject, DestroyDelay);
    }
  }
}