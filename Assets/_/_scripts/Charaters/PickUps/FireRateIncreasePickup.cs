using UnityEngine;

public class FireRateIncreasePickup : MonoBehaviour {
  public float amount;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      // SimplePlayer.instance.CurrentWeapon.FireRate += amount;
      Destroy(gameObject);
    }
  }
}