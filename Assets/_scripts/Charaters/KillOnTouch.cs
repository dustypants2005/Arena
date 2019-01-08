using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants{
  [RequireComponent(typeof(Collider))]
  public class KillOnTouch : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
      if(other.tag != "Player") return;
      var hp = other.GetComponent<Health>();
      if(hp != null){
        var maxHP = hp.MaxHealth;
        hp.AdjustHealth(-maxHP);
      }
    }
  }
}