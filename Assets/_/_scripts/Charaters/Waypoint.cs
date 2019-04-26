using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour {

  public Color gismoColor = new Color(1, 0, 0, 0.5F);
  public gismoType type =  gismoType.sphere;
  public enum gismoType { cube,  sphere }

  void OnDrawGizmos() {
    switch(type){
      case gismoType.cube : {
        Gizmos.color = gismoColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
        break;
      }
      case gismoType.sphere : {
        Gizmos.color = gismoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.x);
        break;
      }
      default:
        Debug.LogError("Should have a type.");
      break;
    }
  }
}
