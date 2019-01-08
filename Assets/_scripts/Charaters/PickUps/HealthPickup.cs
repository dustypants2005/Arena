using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class HealthPickup : MonoBehaviour {
    public int HP = 10;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var health = other.GetComponent<Health>();
        health.AdjustHealth(HP);
        Destroy(gameObject);
      }
    }
  }

}
