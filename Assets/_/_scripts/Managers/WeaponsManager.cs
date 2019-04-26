using dustypants.Characters.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Managers {

  [DisallowMultipleComponent]
  public class WeaponsManager : MonoBehaviour {
    public static WeaponsManager instance;
    public static string FileName = "/Weaponinfo.dat";
    public int WeaponIndex = 0;

    /// <summary>
    /// List of Available Weapons the Player Can use
    /// </summary>
    public List<GameObject> WeaponLibrary = new List<GameObject>();
    /// <summary>
    /// List of Available Projectile Spawns the Player Can use
    /// </summary>
    public List<GameObject> ProjectileSpawnLibrary = new List<GameObject>();

    public List<WeaponSave> WeaponSaves = new List<WeaponSave>();

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

    public GameObject GetWeapon(int index) {
      return WeaponLibrary[index];
    }

    public GameObject GetProjectileSpawn(int index) {
      return ProjectileSpawnLibrary[index];
    }

    #region Save Controls

    public void Save() {
      SaveManager.Save(FileName, WeaponSaves);
    }

    public void Load() {
      var loadinfo = SaveManager.instance.Load(FileName);
      if (loadinfo == null) {
        Debug.LogError("Weapon Load is null");
        return;
      }
      WeaponSaves = (List<WeaponSave>)loadinfo;
    }

    public void Reset() {
      WeaponSaves = new List<WeaponSave> { new WeaponSave { Weapon = 0, ProjectileSpawn = 0 } };
      Save();
    }

    #endregion

    #region Weapon Controls

    /// <summary>
    /// Initalize Player's weapons
    /// </summary>
    public void InitWeapons() {
      Load();
      if (WeaponSaves.Count == 0) {
        WeaponSaves.Add(new WeaponSave { Weapon = 0, ProjectileSpawn = 0 });
        Save();
      }
      foreach (var weapon in WeaponSaves) {
        AddWeapon(weapon);
      }
    }

    public GameObject NextWeapon() {
      if (WeaponSaves.Count - 1 == WeaponIndex) {
        WeaponIndex = 0;
      } else {
        WeaponIndex++;
      }
      UpdateWeapons();
      return GetCurrentWeapon();
    }

    public GameObject PreviousWeapon() {
      if (0 == WeaponIndex) {
        WeaponIndex = (WeaponSaves.Count - 1);
      } else {
        WeaponIndex--;
      }
      UpdateWeapons();
      return GetCurrentWeapon();
    }

    public GameObject GetCurrentWeapon() {
      var i = 0;
      foreach (Transform w in SimplePlayer.instance.WeaponMount) {
        if (i == WeaponIndex) {
          return w.gameObject;
        }
        i++;
      }
      return null;
    }

    /// <summary>
    /// Adding a new weapon to the weapon saves plus AddWeapon()
    /// </summary>
    /// <param name="Weapon"></param>
    /// <param name="ProjectileSpawn"></param>
    public void AddWeaponSave(int weapon, int ps) {
      var w = new WeaponSave { Weapon = weapon, ProjectileSpawn = ps };
      WeaponSaves.Add(w);
      AddWeapon(w);
      Save();
    }

    /// <summary>
    /// Adding JUST the game object to the player
    /// </summary>
    /// <param name="weapon"></param>
    public void AddWeapon(WeaponSave weapon) {
      var w = Instantiate(WeaponLibrary[weapon.Weapon], SimplePlayer.instance.WeaponMount);
      w.transform.localPosition = Vector3.zero;
      UpdateWeapons();
    }

    /// <summary>
    /// Updating the game objects on the player to match the current weapon index.
    /// </summary>
    public void UpdateWeapons() {
      var i = 0;
      foreach (Transform w in SimplePlayer.instance.WeaponMount) {
        if (i == WeaponIndex) {
          w.gameObject.SetActive(true);
        } else {
          w.gameObject.SetActive(false);
        }
        i++;
      }
    }


    public void UpgradeProjectileSpawn() {
      var wantedIndex = ++WeaponSaves[WeaponIndex].ProjectileSpawn;
      if (wantedIndex > ProjectileSpawnLibrary.Count - 1) {
        wantedIndex = ProjectileSpawnLibrary.Count - 1;
      }
      // update the ref
      WeaponSaves[WeaponIndex].ProjectileSpawn = wantedIndex;
      // update the gameobject to load the projectile spawns
      SimplePlayer.instance.CurrentWeapon.SetProjectileSpawn(ProjectileSpawnLibrary[WeaponSaves[WeaponIndex].ProjectileSpawn]);
      Save();
    }
    #endregion
  }
  [Serializable]
  public class WeaponSave {
    public int Weapon { get; set; }
    public int ProjectileSpawn { get; set; }
  }
}