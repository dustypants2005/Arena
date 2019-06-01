using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {
  public static SaveManager instance;

  public PlayerInfo data;
  public static string FileName = "/playerinfo.dat";

  void Awake () {
    if (instance == null) {
      DontDestroyOnLoad (gameObject);
      instance = this;
    } else {
      if (instance != this) {
        Destroy (gameObject);
      }
    }
    if (data == null) {
      data = new PlayerInfo ();
    }
  }
  private void Start () {
    LoadPlayerInfo ();
  }

  public void SaveData (string filename = "/playerinfo.dat") {
    var bf = new BinaryFormatter ();
    var file = File.Create (Application.persistentDataPath + filename);

    bf.Serialize (file, data);
    file.Close ();
  }

  public static void Save (string filename, object data) {
    var bf = new BinaryFormatter ();

    using (var file = File.Create (Application.persistentDataPath + filename)) {
      file.Position = 0;
      bf.Serialize (file, data);
    }
  }

  public void SavePlayer (Transform savePoint) {
    var sp = SimplePlayer.instance;
    var health = sp.GetComponent<Health> ();
    data.Coins = CoinManager.instance.CoinsCollected;
    data.Health = health.CurrentHealth;
    data.MaxHealth = health.MaxHealth;
    data.Spawn = new SpawnPoint (savePoint);
    data.LevelName = SceneManager.GetActiveScene ().name;

    SaveData ();
  }

  public PlayerInfo LoadPlayerInfo (string filename = "/playerinfo.dat") {
    if (File.Exists (Application.persistentDataPath + filename)) {
      var bf = new BinaryFormatter ();
      using (var file = File.Open (Application.persistentDataPath + filename, FileMode.Open)) {
        data = (PlayerInfo) bf.Deserialize (file);
        CoinManager.instance.CoinsCollected = data.Coins;
        return data;
      }
    }
    Debug.Log ("load file does NOT exist");
    return null;
  }

  public object Load (string filename) {
    if (File.Exists (Application.persistentDataPath + filename)) {
      var bf = new BinaryFormatter ();
      using (var file = File.Open (Application.persistentDataPath + filename, FileMode.Open)) {
        var filedata = bf.Deserialize (file);
        return filedata;
      }
    }
    Debug.Log ("load file does NOT exist");
    return null;
  }

  public string GetCurrentLevelName () {
    return SceneManager.GetActiveScene ().name;
  }

  public void Reset () {
    data.Reset ();
    SaveData ();
  }
}

[Serializable]
public class PlayerInfo {
  /// <summary>
  /// Current health of the Player.
  /// </summary>
  public float Health = 100;
  /// <summary>
  ///
  /// </summary>
  public float MaxHealth = 100;
  public int Coins = 0;
  /// <summary>
  /// aka transform, but since I can't serailize transform, i made this.
  /// </summary>
  public SpawnPoint Spawn = null;
  public string LevelName = "Level.1.1";
  /// <summary>
  /// <param name="name">level name</param>
  /// <param name="event">A TriggerEvent</param>
  /// </summary>
  [SerializeField]
  public Dictionary<string, TriggerEvent> UnlockedLevels;

  //TODO: save weapons
  /// <summary>
  /// weaponType, projectileSpawnType
  /// </summary>
  public int[][] weapons;

  // Resets all members except levelname;
  public void Reset () {
    Health = 100;
    MaxHealth = 100;
    Coins = 0;
    Spawn = null;
  }

  /* ----------------
   * player abilities
   * ----------------
   */
  [Header ("Abilities")]
  public bool CanDoubleJump = false;
  public bool CanWallJump = false;
  public bool CanDash = false;
}

[Serializable]
public class SpawnPoint {
  public SpawnPoint (Transform spawn) {
    Position = new float[3];
    Position[0] = spawn.position.x;
    Position[1] = spawn.position.y;
    Position[2] = spawn.position.z;
    Rotation = new float[3];
    Rotation[0] = spawn.eulerAngles.x;
    Rotation[1] = spawn.eulerAngles.y;
    Rotation[2] = spawn.eulerAngles.z;
    Scale = new float[3];
    Scale[0] = spawn.localScale.x;
    Scale[1] = spawn.localScale.y;
    Scale[2] = spawn.localScale.z;
  }
  public float[] Position = new float[3];
  public float[] Rotation = new float[3];
  public float[] Scale = new float[3];
  public string LevelName = "";
  public static SpawnPoint ToSpawnPoint (Transform t) {
    return new SpawnPoint (t);
  }
}