using UnityEngine;

public class ProjectileRangePickup : MonoBehaviour {
  public float amount;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      // SimplePlayer.instance.CurrentWeapon.ProjectilLifespan += amount;
      Destroy(gameObject);
    }
  }
}