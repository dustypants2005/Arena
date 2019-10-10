using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnDestroy : MonoBehaviour {
  UbhShotCtrl shotCtrl;
  void Awake() {
    shotCtrl = GetComponent<UbhShotCtrl>();
  }
  void OnDestroy() {
    shotCtrl.StartShotRoutine();
  }
}