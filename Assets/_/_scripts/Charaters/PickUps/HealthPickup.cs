using UnityEngine;

  [RequireComponent(typeof(Rigidbody))]
  public class HealthPickup : MonoBehaviour {
    public int HP = 10;
    public GameObject pickupEfx;
    public float pickfxSize = 5;
    public float InitJump = 100f;

    void Awake() {
      if(pickupEfx == null) Debug.LogError("No efx");
    }

    void Start() {
      var rb = GetComponent<Rigidbody>();
      rb.AddForce((Vector3.up * InitJump));
    }

    void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var health = other.GetComponent<Health>();
        health.AdjustHealth(HP);
        var efx = Instantiate(pickupEfx, transform.position, transform.rotation);
        efx.transform.localScale = Vector3.one * pickfxSize;
        var anim = efx.GetComponentInChildren<Animator>();
        anim.Play("PickupText");
        Destroy(efx, 1f);
        Destroy(gameObject);
      }
    }
  }