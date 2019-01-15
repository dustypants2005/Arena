using UnityEngine;
using dustypants.Managers;
using TMPro;

namespace dustypants.Utility {
  public class Buy : MonoBehaviour {
    public int Cost;

    /// <summary>
    /// Events fired for purchase.
    /// </summary>
    public TriggerEvent PurchaseEvents;
    /// <summary>
    /// Toggle to show purchase info.
    /// i.e. cost and the button to purchase.
    /// </summary>
    public TriggerEvent Show;
    /// <summary>
    /// Remove purchase info.
    /// </summary>
    public TriggerEvent Hide;

    public TextMeshPro Text;

    private void OnEnable() {
      if(SaveManager.instance.data.UnlockedLevels == null) { return;}
      foreach(var level in SaveManager.instance.data.UnlockedLevels){
        level.Value.Invoke();
      }
    }

    private void OnTriggerEnter(Collider other) {
      if ( other.tag == "Player"){
        Show.Invoke();
      }
    }

    private void OnTriggerExit(Collider other) {
      if ( other.tag == "Player"){
        Hide.Invoke();
      }
    }

    private void OnTriggerStay(Collider other) {
      if ( other.tag == "Player"){
        if(Input.GetButtonUp("X") && PurchaseEvents != null){ // Buy Action
          var coins = CoinManager.instance.Coins;
          if( coins >= Cost ){
            CoinManager.instance.Subtract(Cost);
            LevelManager.instance.Add(Text.text, PurchaseEvents);
            PurchaseEvents.Invoke();
          }
        }
      }
    }
  }
}