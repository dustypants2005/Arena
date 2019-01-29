using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class HealthPickup : MonoBehaviour {
    public int HP = 10;
    public GameObject pickupEfx;
    public float pickefxSize = 5;

    private void Awake() {
      if(pickupEfx == null) Debug.LogError("No efx");
    }

    private void OnTriggerEnter(Collider other) { 
      if(other.CompareTag("Player")){
        var health = other.GetComponent<Health>();
        health.AdjustHealth(HP);
        var efx = Instantiate(pickupEfx, transform.position, transform.rotation);
        efx.transform.localScale = new Vector3(1,1,1) * pickefxSize;
        var anim = efx.GetComponentInChildren<Animator>();
        anim.Play("PickupText");
        Destroy(efx, 1f);
        Destroy(gameObject);
      }
    }

    void OnLevelWasLoaded(int level) {
      Destroy(this.gameObject);
    }
  }
}
