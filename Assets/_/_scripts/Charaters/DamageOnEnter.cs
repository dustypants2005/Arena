using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnter : MonoBehaviour {
  [SerializeField] private bool isTempInvulnerable = false;
  [SerializeField] private float invulnerableTime = 1f;
  [SerializeField] private float damage = 10f;

  void OnTriggerEnter(Collider other) {
    var hp = other.GetComponent<Health>();
    if (hp != null && other.tag == "Player") {
      hp.AdjustHealth(-damage, isTempInvulnerable, invulnerableTime);
    }
  }
}