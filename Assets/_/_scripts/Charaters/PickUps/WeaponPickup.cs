using dustypants.Managers;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class WeaponPickup : MonoBehaviour {
    //public GameObject Pickup;
    public int Weapon = 0;
    public int ProjectileSpawn = 0;

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        //var inv = other.GetComponent<Inventory>();
        //inv.AddWeapon(Pickup);
        WeaponsManager.instance.AddWeaponSave(Weapon, ProjectileSpawn);
        Destroy(gameObject);
      }
    }
  }
}
