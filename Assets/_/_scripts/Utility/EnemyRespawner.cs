using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Componenet for Enemies to tell the spawner to spawn another enemy after this one dies.
/// </summary>
public class EnemyRespawner : MonoBehaviour {
  public EnemySpawner spawner;
  private void OnDestroy () {
    spawner.Respawn ();
  }
}