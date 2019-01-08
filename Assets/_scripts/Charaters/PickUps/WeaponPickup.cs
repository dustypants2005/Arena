using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class WeaponPickup : MonoBehaviour {
    public GameObject Pickup;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var inv = other.GetComponent<Inventory>();
        inv.AddWeapon(Pickup);
        Destroy(gameObject);
      }
    }
  }
}
