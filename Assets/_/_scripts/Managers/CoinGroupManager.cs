using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinGroupManager : MonoBehaviour {
  public static CoinGroupManager instance;

  void Awake() {
    if (instance == null) {
      instance = this;
    } else {
      if (instance != this) {
        Destroy(gameObject);
      }
    }
  }
  void Start() {
    if (CoinManager.instance.CoinSaves == null) {
      Debug.LogError("No Coin Save");
      return;
    }
    if (!CoinManager.instance.CoinSaves.ContainsKey(SceneManager.GetActiveScene().name)) {
      Debug.Log("CoinGroupManger: No coinsave");
      foreach (Transform child in transform) {
        var cp = child.GetComponent<CoinPickup>();
        CoinManager.instance.AddOrUpdate(SceneManager.GetActiveScene().name, cp.ID, true);
      }
      CoinManager.instance.Save();
    } else {
      var states = CoinManager.instance.CoinSaves[SceneManager.GetActiveScene().name];
      foreach (Transform child in transform) {
        var cp = child.GetComponent<CoinPickup>();
        if (states.ContainsKey(cp.ID)) {
          cp.gameObject.SetActive(states[cp.ID]);
        } else {
          CoinManager.instance.AddOrUpdate(SceneManager.GetActiveScene().name, cp.ID, true);
        }
      }
    }
  }
}