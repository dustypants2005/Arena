using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class LinePath : MonoBehaviour {
  public Transform Waypoints;
  LineRenderer line;
  List<Vector3> positions = new List<Vector3>();
  void Awake() {
    if (Waypoints == null) {
      Debug.LogError("No Waypoints on " + transform.name);
    }
    line = GetComponent<LineRenderer>();
    SetPath();
  }

  void SetPath() {
    foreach (Transform waypoint in Waypoints) {
      positions.Add(waypoint.position);
    }
    line.SetPositions(positions.ToArray());
  }
}