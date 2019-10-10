using UnityEngine;

public class WeaponPickup : MonoBehaviour {
  public GameObject Weapon;
  public GameObject PickupFX;

  void Awake() {
    if (Weapon == null)
      Debug.LogError("Weapon Should not be null on WeaponPickup for " + gameObject.name);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      if (PickupFX != null) {
        Instantiate(PickupFX, transform.position, transform.rotation);
      }
      // should not add if weapon exists
      WeaponsManager.instance.AddWeapon(Weapon);
      Destroy(gameObject);
    }
  }
}