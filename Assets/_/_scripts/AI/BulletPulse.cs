using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UbhShotCtrl))]
public class BulletPulse : MonoBehaviour {
  public float intervalTime = 5f;
  UbhShotCtrl shotCtrl;

  float spawnTimer;

  void Awake() {
    shotCtrl = GetComponentInChildren<UbhShotCtrl>();
    SetSpawnTimer();
  }

  void Update() {
    if (Time.time > spawnTimer) {
      shotCtrl.StartShotRoutine();
      SetSpawnTimer();
    }
  }

  void SetSpawnTimer() {
    spawnTimer = Time.time + intervalTime;
  }
}