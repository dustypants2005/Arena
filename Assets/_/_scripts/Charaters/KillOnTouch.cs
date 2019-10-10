using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillOnTouch : MonoBehaviour {
  private void OnTriggerEnter(Collider other) {
    var hp = other.GetComponent<Health>();
    if (hp != null) {
      hp.AdjustHealth(-hp.MaxHealth);
    }
  }
}