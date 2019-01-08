using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class ProjectileRangePickup : MonoBehaviour {
    public float amount;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var inv = other.GetComponent<Inventory>();
        var weapon = inv.GetCurrentWeapon().GetComponent<Weapon>();
        weapon.ProjectilLifespan += amount;

        Destroy(gameObject);
      }
    }
  }
}
