using dustypants.Characters.Pickups;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace dustypants.Managers {
  public class CoinGroupManager : MonoBehaviour {
    void OnEnable() {
      if(!CoinManager.instance.CoinSaves.ContainsKey(SceneManager.GetActiveScene().name)) {
        Debug.Log("CoinGroupManger: No coinsave");
        foreach(Transform child in transform) {
          var cp = child.GetComponent<CoinPickup>();
          CoinManager.instance.AddOrUpdate(SceneManager.GetActiveScene().name, cp.ID, true);
        }
        CoinManager.instance.Save();
      } else {
        var states = CoinManager.instance.CoinSaves[SceneManager.GetActiveScene().name];
        foreach(Transform child in transform) {
          var cp = child.GetComponent<CoinPickup>();
          if(states.ContainsKey(cp.ID)) {
            cp.gameObject.SetActive(states[cp.ID]);
          } else {
            CoinManager.instance.AddOrUpdate(SceneManager.GetActiveScene().name, cp.ID, true);
          }
        }
      }
    }
  }
}
