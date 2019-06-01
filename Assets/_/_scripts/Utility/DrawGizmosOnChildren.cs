using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosOnChildren : MonoBehaviour {
  public Color gizmoColor;
  void OnDrawGizmos () {
    Gizmos.color = gizmoColor;
    foreach (Transform child in transform) {
      Gizmos.DrawWireCube (child.position, child.localScale);
    }
  }
}