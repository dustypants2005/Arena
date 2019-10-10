using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
public class WeaponsManager : MonoBehaviour {
  public static WeaponsManager instance;
  public int WeaponIndex = 0;
  public List<UbhBaseShot> WeaponsList = new List<UbhBaseShot>();

  void Awake() {
    if (instance == null) {
      DontDestroyOnLoad(gameObject);
      instance = this;
    } else {
      if (instance != this) {
        Destroy(gameObject);
      }
    }
  }

  public GameObject NextWeapon() {
    WeaponIndex = ++WeaponIndex % WeaponsList.Count;
    UpdateWeapons();
    return GetCurrentWeapon();
  }

  public GameObject PreviousWeapon() {
    if (0 == WeaponIndex) WeaponIndex = WeaponsList.Count;
    WeaponIndex--;
    UpdateWeapons();
    return GetCurrentWeapon();
  }

  public GameObject GetCurrentWeapon() {
    return WeaponsList[WeaponIndex].gameObject;
  }

  /// <summary>
  /// Adding JUST the game object to the player
  /// </summary>
  /// <param name="shotOBJ"></param>
  public void AddWeapon(GameObject shotOBJ) {
    /**
     * TODO: need to make sure we can not add the same weapon twice.
     */
    var s = shotOBJ.GetComponent<UbhBaseShot>();
    if (s.NullCheck()) {
      Debug.LogError("Can't Add Weapon Without UBHBaseShot!");
      return;
    }
    shotOBJ.SetActive(false);
    var shot = Instantiate(shotOBJ, SimplePlayer.instance.transform);
    // WeaponIndex = GetWeapons().FindIndex(w => w == shot);
    UpdateWeaponsList();
  }

  /// <summary>
  /// Updating the game objects on the player to match the current weapon index. DOES NOT UPDATE LIST!
  /// </summary>
  public void UpdateWeapons() {
    var i = 0;
    GetShotCtrl().StopShotRoutineAndPlayingShot();
    var shots = GetWeapons();
    foreach (var shot in shots) {
      shot.gameObject.SetActive(i == WeaponIndex);
      i++;
    }
    AssignShotCtrl();
  }

  public void AssignShotCtrl() {
    GetShotCtrl().m_shotList[0].m_shotObj = GetCurrentWeapon().GetComponent<UbhBaseShot>();
  }

  public void UpdateWeaponsList() {
    WeaponsList.Clear();
    foreach (Transform child in SimplePlayer.instance.transform) {
      var baseshot = child.GetComponent<UbhBaseShot>();
      if (baseshot != null) {
        WeaponsList.Add(baseshot);
      }
    }
    AssignShotCtrl();
  }

  public List<UbhBaseShot> GetWeapons() {
    var weapons = new List<UbhBaseShot>();
    foreach (Transform child in SimplePlayer.instance.transform) {
      var baseshot = child.GetComponent<UbhBaseShot>();
      if (baseshot != null) {
        weapons.Add(baseshot);
      }
    }
    return weapons;
  }

  public UbhShotCtrl GetShotCtrl() {
    return SimplePlayer.instance.GetComponent<UbhShotCtrl>();
  }
}