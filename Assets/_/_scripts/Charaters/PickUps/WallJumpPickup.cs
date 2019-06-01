using UnityEngine;


  public class WallJumpPickup : MonoBehaviour {
    public GameObject pickupEfx;
    public float pickfxSize = 5;

    void Start() {
      if (SaveManager.instance.data.CanWallJump) {
        gameObject.SetActive(false);
      }
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        SimplePlayer.instance.Info.CanWallJump = true;
        SaveManager.instance.data.CanWallJump = true;
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