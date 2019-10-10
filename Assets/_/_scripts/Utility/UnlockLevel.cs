using TMPro;
using UnityEngine;

public class UnlockLevel : MonoBehaviour {
  public int Cost;
  public TriggerEvent Show;
  public TriggerEvent Hide;
  [SerializeField] private TextMeshPro CostText;

  void Start() {
    var coins = CoinManager.instance.CoinsCollected;
    if (coins >= Cost) {
      Show.Invoke();
    } else {
      Hide.Invoke();
      if (CostText != null) {
        CostText.text = "$" + Cost;
      }
    }
  }
}