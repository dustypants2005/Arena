using dustypants.Characters.Player;
using dustypants.Managers;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class DashPickup : MonoBehaviour {
    public GameObject pickupEfx;
    public float pickfxSize = 5;

    void Start() {
      if (SaveManager.instance.data.CanDash) {
        gameObject.SetActive(false);
      }
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        SimplePlayer.instance.Info.CanDash = true;
        SaveManager.instance.data.CanDash = true;
        SaveManager.instance.SaveData();
        if (pickupEfx != null) {
          var efx = Instantiate(pickupEfx, transform.position, transform.rotation);
          efx.transform.localScale = Vector3.one * pickfxSize;
          var anim = efx.GetComponentInChildren<Animator>();
          anim.Play("PickupText");
          Destroy(efx, 1f);
        }
        Destroy(gameObject);
      }
    }
  }
}