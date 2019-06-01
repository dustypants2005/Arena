using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

  public static LevelManager instance;

  private void Awake () {
    if (instance == null) {
      DontDestroyOnLoad (gameObject);
      instance = this;
    } else {
      if (instance != this) {
        Destroy (gameObject);
      }
    }
  }

  private void OnEnable () {
    if (SaveManager.instance.data.UnlockedLevels == null) { return; }
    foreach (var level in SaveManager.instance.data.UnlockedLevels) {
      level.Value.Invoke ();
    }
  }

  public void Add (string name, TriggerEvent e) {
    SaveManager.instance.data.UnlockedLevels.Add (name, e);
    SaveManager.instance.SaveData (); // save the newly unlocked level
  }

  public bool Remove (string name) {
    return SaveManager.instance.data.UnlockedLevels.Remove (name);
  }

  public bool Contains (string name) {
    return SaveManager.instance.data.UnlockedLevels.ContainsKey (name);
  }

  public void Clear () {
    SaveManager.instance.data.UnlockedLevels.Clear ();
  }
}