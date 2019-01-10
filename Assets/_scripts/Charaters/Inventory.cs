using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants {

  [DisallowMultipleComponent]
  public class Inventory : MonoBehaviour {
    public GameObject WeaponMount;
    public List<GameObject> Weapons = new List<GameObject>();
    public GameObject[] StartingWeapons;
    public int CurrentWeapon;
    /// <summary>
    /// A list of all the Weapons the player can use.
    /// </summary>
    public GameObject[] WeaponsCatalog;
    /// <summary>
    /// A list of all the projectile spawns the player could have assigned.
    /// </summary>
    public GameObject[] ProjectileSpawnCatalog;

    public void Init(){
      // TODO: set up save
      foreach(GameObject weapon in StartingWeapons){
        AddWeapon(weapon);
      }
    }

    public void AddWeapon(GameObject weapon) {
      var w = Instantiate(weapon, WeaponMount.transform);
      w.transform.localPosition = Vector3.zero;
      Weapons.Add(w);
      UpdateWeapons();
    }
    public void Remove(int weaponNumber) {
      var weapon = Weapons[weaponNumber];
      Weapons.Remove(weapon);
      Destroy(weapon);
      UpdateWeapons();
    }

    public GameObject NextWeapon()
    {
      if (Weapons.Count - 1 == CurrentWeapon){
        CurrentWeapon = 0;
      }else{
        CurrentWeapon++;
      }
      UpdateWeapons();
      return Weapons[CurrentWeapon];
    }

    public GameObject PreviousWeapon(){
      if (0 == CurrentWeapon){
        CurrentWeapon = (Weapons.Count - 1);
      }else{
        CurrentWeapon--;
      }
      UpdateWeapons();
      return Weapons[CurrentWeapon];
    }

    public GameObject GetCurrentWeapon(){
      return Weapons[CurrentWeapon];
    }

    void UpdateWeapons(){
      var i = 0;
      foreach(Transform weapon in WeaponMount.transform){
        if(i == CurrentWeapon){
          weapon.gameObject.SetActive(true);
        } else{
          weapon.gameObject.SetActive(false);
        }
        i++;
      }
    }
  }
}
