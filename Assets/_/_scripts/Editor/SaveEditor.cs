﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveEditor : EditorWindow {

  [MenuItem("Window/SaveManager")]
  public static void ShowSaveManager() {
    GetWindow<SaveEditor>("SaveManager").Show();
  }

  private void OnGUI() {
    if (GUILayout.Button("Reset All")) {
      SaveManager.Save(SaveManager.FileName, new PlayerInfo());
      SaveManager.Save(CoinManager.FileName, new Dictionary<string, Dictionary<float, bool>>());
    }
    if (GUILayout.Button("Reset Saves")) {
      SaveManager.Save(SaveManager.FileName, new PlayerInfo());
    }
    if (GUILayout.Button("Reset Coins")) {
      SaveManager.Save(CoinManager.FileName, new Dictionary<string, Dictionary<float, bool>>());
    }
  }
}