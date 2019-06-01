using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
  public Health health;
  void Start () {
    if(health == null){
      Debug.LogError("No Health assigned for damagable.");
    }
  }

  public void AdjustHealth(float adjustment, bool setInvulnerable = false, float invulnerableTime = 1f){
    health.AdjustHealth(adjustment, setInvulnerable, invulnerableTime);
  }
}

