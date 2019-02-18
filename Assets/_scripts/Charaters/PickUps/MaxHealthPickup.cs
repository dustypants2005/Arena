using UnityEngine;

namespace dustypants.Characters.Pickups {
  [RequireComponent(typeof(Rigidbody))]
  public class MaxHealthPickup : MonoBehaviour {
    public int HP = 10;
    public GameObject pickupEfx;
    public float pickefxSize = 5;
    public float InitJump = 500f;

    void Start() {
      var rb = GetComponent<Rigidbody>();
      rb.AddForce((Vector3.up * InitJump));
    }

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var health = other.GetComponent<Health>();
        health.AdjustHealth(HP);
        var efx = Instantiate(pickupEfx, transform.position, transform.rotation);
        efx.transform.localScale = new Vector3(1, 1, 1) * pickefxSize;
        Destroy(efx, 2f);
        Destroy(gameObject);
      }
    }
  }

}
