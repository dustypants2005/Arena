using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants;

namespace dustypants.Characters.Pickups {
  public class ProjectileSpawnPickup : MonoBehaviour {
    public GameObject projectileSpawn;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var inv = other.GetComponent<Inventory>();
        var weapon = inv.GetCurrentWeapon().GetComponent<Weapon>();
        weapon.SetProjectileSpawn(projectileSpawn);
        Destroy(gameObject);
      }
    }
  }
}