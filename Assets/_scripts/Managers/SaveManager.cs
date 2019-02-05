using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using dustypants.Characters;
using dustypants.Utility;

namespace dustypants.Managers {
  public class SaveManager : MonoBehaviour {
    public static SaveManager instance;

    public PlayerInfo data;

    void Awake() {
      if(instance == null){
        DontDestroyOnLoad(gameObject);
        instance = this;
      } else {
        if(instance != this){
          Destroy(gameObject);
        }
      }
      if(data == null){
        data = new PlayerInfo();
      }
    }

    public void Save( string filename = "/playerinfo.dat"){
      var bf = new BinaryFormatter();
      var file = File.Create(Application.persistentDataPath + filename);

      bf.Serialize(file, data);
      file.Close();
    }

    public void SavePlayer(Transform savePoint){
      var sp = SimplePlayer.instance;
      var newData = new PlayerInfo {
        Coins = CoinManager.instance.CoinsCollected,
        Health = sp.GetComponent<Health>().CurrentHealth,
        MaxHealth = sp.GetComponent<Health>().MaxHealth,
        Spawn = new SpawnPoint(savePoint),
        LevelName = SceneManager.GetActiveScene().name
      };
      newData.Spawn.LevelName = newData.LevelName;
      data = newData;
      Save();
    }

    public PlayerInfo Load( string filename = "/playerinfo.dat"){
      if(File.Exists(Application.persistentDataPath + filename)){
        var bf = new BinaryFormatter();
        var file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
        data = (PlayerInfo)bf.Deserialize(file);
        CoinManager.instance.CoinsCollected = data.Coins;
        file.Close();
        return data;
      }
      Debug.Log("load file does NOT exist");
      return null;
    }

    public string GetCurrentLevelName() {
      return SceneManager.GetActiveScene().name;
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
    public Dictionary<string,TriggerEvent> UnlockedLevels;

    //TODO: save weapons
    /// <summary>
    /// weaponType, projectileSpawnType
    /// </summary>
    public int[][] weapons;

    // Resets all members except levelname;
    public void Reset(){
      Health = 100;
      MaxHealth = 100;
      Coins = 0;
      Spawn = null;
    }

    /* ----------------
     * player abilities
     * ----------------
     */

    public bool CanDoubleJump = false;
    public bool CanWallJump = false;
    public bool CanDash = false;
  }

  [Serializable]
  public class SpawnPoint {
    public SpawnPoint( Transform spawn){
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
    public float[] Position;
    public float[] Rotation;
    public float[] Scale;
    public string LevelName = "";
    public static SpawnPoint ToSpawnPoint(Transform t){
      return new SpawnPoint(t);
    }
  }
}