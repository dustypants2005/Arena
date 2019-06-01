using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
  public GameObject GO;
  [SerializeField] private bool freezeAll = true;
  private bool isSpawning = false;
  public void Spawn (float t) {
    if (!isSpawning)
      StartCoroutine (Respawn (t));
    isSpawning = true;
  }

  IEnumerator Respawn (float t) {
    yield return new WaitForSeconds (t);
    if (isSpawning) {
      var fp = Instantiate (GO, transform).GetComponent<FallingPlatform> ();
      fp.spawner = this;
      isSpawning = false;
      var rb = fp.GetComponent<Rigidbody> ();
      if (rb != null) {
        if (freezeAll)
          rb.constraints = RigidbodyConstraints.FreezeAll;
      }
    }
  }
}