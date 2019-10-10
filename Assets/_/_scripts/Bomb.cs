using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Bomb : MonoBehaviour {
  public GameObject radius;
  public GameObject ExplosionFX;
  [SerializeField] private float fxDestroyTimer = 5f;

  void OnDestroy() {
    SpawnFX();
  }

  void SpawnFX() {
    var fx = Instantiate(ExplosionFX, transform.position, transform.rotation);
    Destroy(fx, fxDestroyTimer);
  }
}