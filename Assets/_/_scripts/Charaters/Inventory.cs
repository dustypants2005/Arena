using dustypants.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants {

  [DisallowMultipleComponent]
  public class Inventory : MonoBehaviour {
    public GameObject WeaponMount;

    public List<GameObject> Weapons = new List<GameObject>();
    public GameObject[] StartingWeapons;
    public int CurrentWeapon;

    private const string _FileName = "/weapons.dat";

    public void Init() {
      foreach (GameObject weapon in StartingWeapons) {
        AddWeapon(weapon);
      }
    }

    public void AddWeapon(GameObject weapon) {
      var w = Instantiate(weapon, WeaponMount.transform);
      w.transform.localPosition = Vector3.zero;
      Weapons.Add(w);
      UpdateWeapons();
      //SaveWeapons();
    }
    public void Remove(int weaponNumber) {
      var weapon = Weapons[weaponNumber];
      Weapons.Remove(weapon);
      Destroy(weapon);
      UpdateWeapons();
      //SaveWeapons();
    }

    public GameObject NextWeapon() {
      if (Weapons.Count - 1 == CurrentWeapon) {
        CurrentWeapon = 0;
      } else {
        CurrentWeapon++;
      }
      UpdateWeapons();
      return Weapons[CurrentWeapon];
    }

    public GameObject PreviousWeapon() {
      if (0 == CurrentWeapon) {
        CurrentWeapon = (Weapons.Count - 1);
      } else {
        CurrentWeapon--;
      }
      UpdateWeapons();
      return Weapons[CurrentWeapon];
    }

    public GameObject GetCurrentWeapon() {
      return Weapons[CurrentWeapon];
    }

    void UpdateWeapons() {
      var i = 0;
      foreach (Transform weapon in WeaponMount.transform) {
        if (i == CurrentWeapon) {
          weapon.gameObject.SetActive(true);
        } else {
          weapon.gameObject.SetActive(false);
        }
        i++;
      }
    }

    /// <summary>
    /// Save Player's weapons
    /// </summary>
    public void SaveWeapons() {
      if (Weapons == null) {
        Debug.Log("Null Weapons");
        return;
      }

      if (SaveManager.instance == null) {
        Debug.Log("Null SaveManager.instance");
        return;
      }
      SaveManager.Save(_FileName, Weapons);
    }

    /// <summary>
    /// Load Player's Weapons
    /// </summary>
    public bool LoadWeapons() {
      if (SaveManager.instance == null) {
        Debug.Log("SaveManager null");
        return false;
      }
      var data = SaveManager.instance.Load(_FileName);
      if (data == null) {
        Debug.Log("No Weapons Saved.");
        return false;
      }
      var parsedData = (List<GameObject>)data;
      if (parsedData.Count == 0) {
        Debug.Log("No Weapons Saved.");
        return false;
      }
      Weapons = parsedData;
      return true;
    }
  }
}
