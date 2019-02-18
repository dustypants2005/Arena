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
    switch(type) {
      case gismoType.wireCube: {
        Gizmos.color = gismoColor;
        Gizmos.DrawWireCube(Position + transform.position, Scale);
        break;
      }
      case gismoType.wireSphere:
      {
        Gizmos.color = gismoColor;
        Gizmos.DrawWireSphere(Position + transform.position, Scale.x);
        break;
      }
      case gismoType.cube:
      {
        Gizmos.color = gismoColor;
        Gizmos.DrawCube(Position + transform.position, Scale);
        break;
      }
      case gismoType.sphere:
      {
        Gizmos.color = gismoColor;
        Gizmos.DrawSphere(Position + transform.position, Scale.x);
        break;
      }
      default:
        break;
    }
  }
}
