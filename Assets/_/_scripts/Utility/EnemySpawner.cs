using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawner for Enemies
/// </summary>
[RequireComponent (typeof (Collider))]
public class EnemySpawner : MonoBehaviour {
  [SerializeField] private GameObject enemyObject;
  [SerializeField] private Transform spawnPoint;
  /// <summary>
  /// The max number of enemies we can spawn
  /// </summary>
  [Min (1)][SerializeField] private int maxSpawnCount = 1;
  [SerializeField] private float spawnDelay = 1f;
  /// <summary>
  /// How many enemies we currently have spawned
  /// </summary>
  private int spawnCount = 0;

  private float timer;
  private bool isSpawnObstructed = false;

  private void Awake () {
    var e = enemyObject.GetComponent<Enemy> ();
    if (e == null) {
      Debug.LogError ("Must be Enemy to spawn");
    }

    if (spawnPoint == null) {
      Debug.LogError ("No spawn point!");
    }
  }

  void Start () {

  }

  void Update () {
    if (!isSpawnObstructed &&
      spawnCount < maxSpawnCount &&
      timer < Time.time) {
      Spawn ();
    }
  }

  public void Spawn () {
    var s = Instantiate (enemyObject, spawnPoint.position, spawnPoint.rotation);
    SetTimer ();
    spawnCount++;
    var rs = s.AddComponent<EnemyRespawner> ();
    rs.spawner = this;
  }

  public void Respawn () {
    SetTimer ();
    spawnCount--;
  }

  void SetTimer () {
    if (timer < Time.time) {
      timer = Time.time + spawnDelay;
    }
  }

  void OnTriggerEnter (Collider other) {
    var e = other.GetComponent<Enemy> ();
    if (e != null) {
      isSpawnObstructed = true;
    }
  }

  void OnTriggerExit (Collider other) {
    var e = other.GetComponent<Enemy> ();
    if (e != null) {
      isSpawnObstructed = false;
    }
  }
}