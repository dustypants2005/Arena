﻿using UnityEngine;

public class ProjectileSpawnPickup : MonoBehaviour {
  public GameObject projectileSpawn;
  public float RotateSpeed = 30f;

  void Update() {
    transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.World);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      //var inv = other.GetComponent<Inventory>();
      //var weapon = inv.GetCurrentWeapon().GetComponent<Weapon>();
      //weapon.SetProjectileSpawn(projectileSpawn);
      // WeaponsManager.instance.UpgradeProjectileSpawn();

      //TODO: need to create something for this.
      Destroy(gameObject);
    }
  }
}