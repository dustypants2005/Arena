using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Utility {
  public class Respawn : MonoBehaviour {
    public Spawner RespawnObject;
    public float RespawnTime = 5;
    private void OnDestroy() {
      RespawnObject.Spawn(RespawnTime);
    }
  }
}