using UnityEngine;
using dustypants.Managers;
using TMPro;

namespace dustypants.Utility {
  public class UnlockLevel : MonoBehaviour {
    public int Cost;
    public TriggerEvent Show;
    public TriggerEvent Hide;
    [SerializeField] private TextMeshPro CostText;

    private void OnEnable() {
      var coins = CoinManager.instance.CoinsCollected;
      if(coins >= Cost){
        Show.Invoke();
      } else {
        Hide.Invoke();
        if(CostText != null) {
          CostText.text = "$" + Cost;
        }
      }
    }
  }
}