using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants.Characters.Player;

namespace dustypants {
  [RequireComponent(typeof(Collider))]
  public class DamageOnTouch : MonoBehaviour {
    [SerializeField] private bool isTempInvulnerable = false;
    [SerializeField] private float invulnerableTime = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float knockback = 10;

    void OnTriggerStay (Collider other) {
      var hp = other.GetComponent<Health>();
      if(hp != null && other.tag == "Player"){
        hp.AdjustHealth(-damage, isTempInvulnerable, invulnerableTime);
        SimplePlayer.instance.knockback += new Vector3(0, knockback, -knockback);
      }
    }
  }
}

