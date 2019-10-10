using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
  public string text = "spawned";
  void Awake() {
    Debug.Log(text);
  }

  void OnDestroy() {
    Debug.Log(text + " " + gameObject.name);
  }
}