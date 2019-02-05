using dustypants.Managers;
using UnityEngine;

namespace dustypants.Characters.Pickups {
  public class DoubleJumpPickup : MonoBehaviour  {
    public GameObject pickupEfx;
    public float pickfxSize = 5;

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")) {
        SimplePlayer.instance.Info.CanDoubleJump = true;
        SaveManager.instance.data.CanDoubleJump = true;
        SaveManager.instance.Save();
        if(pickupEfx != null)
        {
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
