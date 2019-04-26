using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
  public GameObject crosshair;
  public Vector3 Offset;

  public void AdjustCrosshair(Vector3 position){
    var pos = transform.GetComponentInChildren<Camera>().WorldToScreenPoint(position);
    crosshair.transform.position = pos;
    crosshair.transform.position += Offset;
  }
}
