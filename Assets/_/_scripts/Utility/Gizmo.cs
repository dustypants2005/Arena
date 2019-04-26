using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour
{
  public Color gismoColor = new Color(1, 0, 0, 0.5F);
  public Vector3 Position = Vector3.one;
  /// <summary>
  /// if Sphere or wireSphere, use X for radius.
  /// </summary>
  public Vector3 Scale = Vector3.one;
  public gismoType type = gismoType.sphere;
  public enum gismoType { cube, sphere, wireCube, wireSphere }

  private void OnDrawGizmos() {
    Gizmos.matrix = transform.localToWorldMatrix;
    Gizmos.color = gismoColor;
    switch(type) {
      case gismoType.wireCube: {
        Gizmos.DrawWireCube(Position, Scale);
        break;
      }
      case gismoType.wireSphere: {
        Gizmos.DrawWireSphere(Position, Scale.x);
        break;
      }
      case gismoType.cube: {
        Gizmos.DrawCube(Position, Scale);
        break;
      }
      case gismoType.sphere: {
        Gizmos.DrawSphere(Position, Scale.x);
        break;
      }
      default:
        break;
    }
  }
}
