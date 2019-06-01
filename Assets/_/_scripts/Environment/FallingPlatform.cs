using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class FallingPlatform : MonoBehaviour {
  public float FallDelay = 1;
  public float DestroyDelay = 1;
  public Spawner spawner;
  public float respawnTime = 1;
  private Rigidbody rb;
  public TriggerEvent TriggerEvent;

  private void Start () {
    rb = GetComponent<Rigidbody> ();
  }

  private void OnTriggerEnter (Collider other) {
    if (other.CompareTag ("Player")) {
      if (other.transform.position.y < transform.position.y) return;
      rb.constraints = RigidbodyConstraints.None;
      TriggerEvent.Invoke ();
      StartCoroutine (Drop ());
    }
  }

  IEnumerator Drop () {
    yield return new WaitForSeconds (FallDelay);
    if (spawner != null) {
      spawner.Spawn (respawnTime);
    }
    rb.useGravity = true;
    Destroy (gameObject, DestroyDelay);
  }
}