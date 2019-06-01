using UnityEngine;

public class FireRateIncreasePickup : MonoBehaviour {
  public float amount;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      //var inv = other.GetComponent<Inventory>();
      //var weapon = inv.GetCurrentWeapon().GetComponent<Weapon>();
      SimplePlayer.instance.CurrentWeapon.FireRate += amount;

      Destroy(gameObject);
    }
  }
}