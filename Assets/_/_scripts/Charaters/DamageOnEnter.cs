using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnter : MonoBehaviour {
  [SerializeField] private bool isTempInvulnerable = false;
  [SerializeField] private float invulnerableTime = 1f;
  [SerializeField] private float damage = 10f;

  void OnTriggerEnter(Collider other) {
    var damageable = other.GetComponent<Damageable>();
    if (damageable != null) {
      damageable.AdjustHealth(-damage, isTempInvulnerable, invulnerableTime);
    }
    if (other.CompareTag("Destructible")) {
      Destroy(other.gameObject);
    }
  }
}