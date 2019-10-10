using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {
  public Health health;
  void Awake() {
    if (health == null) {
      Debug.LogError("No Health assigned for damagable.");
    }
  }

  public void AdjustHealth(float adjustment, bool setInvulnerable = false, float invulnerableTime = 1f) {
    health.AdjustHealth(adjustment, setInvulnerable, invulnerableTime);
  }
}