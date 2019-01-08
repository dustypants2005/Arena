using UnityEngine;
using dustypants.Managers;
using TMPro;

namespace dustypants.Utility {
  public class Buy : MonoBehaviour {
    public int Cost;
    public TriggerEvent e;
    // Show when you can trigger e
    public TriggerEvent Show;
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
        if(Input.GetButtonUp("X") && e != null){ // Buy Action
          var coins = CoinManager.instance.Coins;
          if( coins >= Cost ){
            CoinManager.instance.Subtract(Cost);
            LevelManager.instance.Add(Text.text, e);
            e.Invoke();
          }
        }
      }
    }
  }
}