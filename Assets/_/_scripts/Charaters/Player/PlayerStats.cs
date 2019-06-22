using UnityEngine;

public class PlayerStats : MonoBehaviour {
  public PlayerInfo Info {
    get {
      if (SaveManager.instance != null) return SaveManager.instance.data;
      return new PlayerInfo();
    }
  }
}