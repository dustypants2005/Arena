using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStart : MonoBehaviour {
  public GameObject PlayerObject;
  public TriggerEvent Show;
  public TriggerEvent Hide;
  private Vector3 positionOffset;

  void Awake() {
    positionOffset = Vector3.up * 2;
  }

  void Start() {
    var player = SimplePlayer.instance;
    var playerInfo = SaveManager.instance.data;
    if (player != null) { // we have player
      Destroy(player);
    }
    Instantiate(PlayerObject, transform.position + positionOffset, transform.rotation);
    WeaponsManager.instance.WeaponIndex = 0;
    CoinManager.instance.UpdateUI();
  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      Show.Invoke();
    }
  }

  void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      Hide.Invoke();
    }
  }
}