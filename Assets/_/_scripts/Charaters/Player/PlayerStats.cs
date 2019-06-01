using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public PlayerInfo Info {get;  private set;}

  public void UpdateInfo() {
    var i = SaveManager.instance.data;
    if (i != null) {
      Info = i;
    } else {
      Info = new PlayerInfo();
    }
  }
}