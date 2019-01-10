﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants;

namespace dustypants.Characters.Pickups {
  public class DamageIncreasePickup : MonoBehaviour {
      public float amount;

      private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
          var inv = other.GetComponent<Inventory>();
          var weapon = inv.GetCurrentWeapon().GetComponent<Weapon>();
          weapon.Damage += amount;

          Destroy(gameObject);
        }
      }
  }
}