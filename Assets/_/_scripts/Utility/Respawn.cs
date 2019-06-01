using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
  public Spawner RespawnObject;
  public float RespawnTime = 5;
  private void OnDestroy () {
    RespawnObject.Spawn (RespawnTime);
  }
}