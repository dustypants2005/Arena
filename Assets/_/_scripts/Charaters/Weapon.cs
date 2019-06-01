using System.Collections.Generic;
using UnityEngine;


  [DisallowMultipleComponent]
  public class Weapon : MonoBehaviour {
    public bool isSingleShot = true;
    public int ProjectileCount = 1;
    public bool isHoming = false;
    public float homingDamping = 0f;
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f;
    public float ImpactForce = 30;
    public float ProjectileSpeed = 1000;
    public float ProjectilLifespan = 5f;
    public Projectile Projectile;
    public bool LowerVolume = true;
    public string TagToDamage = "Enemy";
    [Tooltip("GameObject with children transforms for spawning projectiles")]
    public GameObject ProjectileSpawnObject;
    public List<Transform> ProjectileSpawns = new List<Transform>();

    public BulletTypes BulletType = BulletTypes.Straight;
    public Vector3 BulletDirection = Vector3.zero;
    public float BulletTypeStrength = 1;

    public enum BulletTypes { Straight, Wave } // TODO: need more bullet types

    public Transform ProjectileMount;
    private float nextTimeToFire { get; set; }
    private CharacterController controller;

    void Start() {
      ResetFireTime();
      Projectile.LowerVolume = LowerVolume;
      if (ProjectileMount == null) Debug.LogError("Projectile Mount is null");
      SetProjectileSpawn();
    }

    public void Attack() {
      if (Time.time > nextTimeToFire) {
        SpawnProjectiles();
      }
    }

    public void ResetFireTime() {
      nextTimeToFire = 0f;
    }

    public void SetController(CharacterController _controller) {
      controller = _controller;
    }

    public void SetProjectileSpawn(GameObject spawnObject) {
      // remove the old
      foreach (Transform go in ProjectileMount) { // remove any game object on the projectile mount
        Destroy(go.gameObject);
      }
      ProjectileSpawns.Clear(); // clear the list of spawns
      //add the new
      var spawn = Instantiate(spawnObject, ProjectileMount);
      foreach (Transform s in spawn.transform) { // set the new spawns
        ProjectileSpawns.Add(s);
      }
    }

    public void SetProjectileSpawn() {
      // remove the old
      foreach (GameObject go in ProjectileMount) { // remove any game object on the projectile mount
        Destroy(go);
      }
      ProjectileSpawns.Clear(); // clear the list of spawns
      //add the new
      var spawn = Instantiate(ProjectileSpawnObject, ProjectileMount);
      foreach (Transform s in spawn.transform) { // set the new spawns
        ProjectileSpawns.Add(s);
      }
      // ProjectileCount = ProjectileMount.childCount;
    }
    public void SpawnProjectiles() {
      var i = 0;
      foreach (Transform spawn in ProjectileSpawns) {
        // if(i > ProjectileCount) {
        //   return;
        // }

        var projectile = Instantiate(Projectile.gameObject, spawn.position, spawn.rotation) as GameObject;
        if (isHoming) {
          var homing = projectile.AddComponent<Homing>();
          if (homingDamping > 0) {
            homing.damping = homingDamping;
            homing.speed = ProjectileSpeed;
          }
        }
        var p = projectile.GetComponent<Projectile>();
        p.Damage = Damage;
        p.TagToDamage = TagToDamage;
        var force = controller == null
        ? projectile.transform.forward * ProjectileSpeed
        : projectile.transform.forward * ProjectileSpeed + controller.velocity;

        switch (BulletType) {
          case BulletTypes.Straight: {
            // do nothing, the bullet shoots straight w/o modification
            break;
          }
          case BulletTypes.Wave: {
            var addedForce = Mathf.Sin(Time.time) * (360 + (i / ProjectileSpawns.Count) * 180);
            force += BulletDirection * addedForce * BulletTypeStrength;
            break;
          }
          default:
            Debug.LogError("Missing Bullet Type");
            break;
        }
        projectile.GetComponent<Rigidbody>().AddForce(force);
        Destroy(projectile, ProjectilLifespan);
        nextTimeToFire = Time.time + 1f / FireRate;
        i++;
      }
    }

  }