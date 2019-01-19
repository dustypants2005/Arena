using UnityEngine;
using dustypants.Managers;

namespace dustypants.Utility {
  public class UnlockLevel : MonoBehaviour {
    public int Cost;
    public TriggerEvent Show;
    public TriggerEvent Hide;

    private void OnEnable() {
      var coins = CoinManager.instance.CoinsCollected;
      if(coins > Cost){
        Show.Invoke();
      } else {
        Hide.Invoke();
      }
    }
  }
}