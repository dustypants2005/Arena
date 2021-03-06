﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CoinManager : MonoBehaviour {
  public static CoinManager instance;
  /// <summary>
  /// Total amount of coins the Player has collected
  /// </summary>
  public int CoinsCollected = 0;
  /// <summary>
  /// { level name : { ID : Active State ( true show, false hide) } }
  /// </summary>
  public Dictionary<string, Dictionary<float, bool>> CoinSaves;

  public static string FileName = "/coininfo.dat";

  private void Awake () {
    if (instance == null) {
      DontDestroyOnLoad (gameObject);
      instance = this;
    } else {
      if (instance != this) {
        Destroy (gameObject);
      }
    }
    LoadSave ();
  }
  private void Start () {
    UpdateUI ();
  }

  /// <summary>
  /// Add the value to the CoinsCollected
  /// </summary>
  /// <param name="amount"></param>
  public void Add (int amount) {
    CoinsCollected += amount;
    SaveManager.instance.data.Coins = CoinsCollected;
    SaveManager.instance.SaveData ();
    UpdateUI ();
  }

  public void Subtract (int amount) {
    CoinsCollected -= amount;
    if (CoinsCollected < 0) CoinsCollected = 0;
    SaveManager.instance.data.Coins = CoinsCollected;
    SaveManager.instance.SaveData ();
    UpdateUI ();
  }

  public void Reset () {
    CoinsCollected = 0;
    UpdateUI ();
    CoinSaves = new Dictionary<string, Dictionary<float, bool>> ();
    Save ();
  }

  public void UpdateUI () {
    if (SimplePlayer.instance != null) {
      SimplePlayer.instance.UpdateCoins (CoinsCollected);
    }
  }

  /// <summary>
  /// Add or update the state of the coin
  /// </summary>
  /// <param name="levelName"></param>
  /// <param name="id"></param>
  /// <param name="state"></param>
  public void AddOrUpdate (string levelName, float id, bool state) {
    if (CoinSaves.ContainsKey (levelName)) {
      if (CoinSaves[levelName].ContainsKey (id)) {
        CoinSaves[levelName][id] = state;
      } else {
        CoinSaves[levelName].Add (id, state);
      }
    } else {
      var c = new Dictionary<float, bool> ();
      c.Add (id, state);
      CoinSaves.Add (levelName, c);
    }
    Save ();
    if (SimplePlayer.instance != null) {
      SimplePlayer.instance.UpdateCoins (CoinsCollected);
    } else {
      Debug.LogError ("No Player instance when trying to update coins.");
    }
  }

  /// <summary>
  /// write to file
  /// </summary>
  public void Save () {
    var bf = new BinaryFormatter ();
    var file = File.Create (Application.persistentDataPath + FileName);
    bf.Serialize (file, CoinSaves);
    file.Close ();
  }

  /// <summary>
  /// Auto Load saved data to CoinSaves
  /// </summary>
  public void LoadSave () {
    CoinSaves = Load ();
  }

  /// <summary>
  /// Retrieve saved data.
  /// </summary>
  /// <returns> { levelname : { id : state} }</returns>
  public Dictionary<string, Dictionary<float, bool>> Load () {
    if (File.Exists (Application.persistentDataPath + FileName)) {
      var bf = new BinaryFormatter ();
      var file = File.Open (Application.persistentDataPath + FileName, FileMode.Open);
      var c = (Dictionary<string, Dictionary<float, bool>>) bf.Deserialize (file);
      file.Close ();
      return c;
    }
    Debug.Log ("load file does NOT exist");
    return new Dictionary<string, Dictionary<float, bool>> ();
  }
}