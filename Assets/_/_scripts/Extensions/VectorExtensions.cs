using UnityEngine;

public static class VectorExtensions {
  public static float Distance(this Vector3 original, Vector3 target){
    return original.Heading(target).magnitude;
  }

  public static Vector3 Heading(this Vector3 original, Vector3 target){
    return target - original;
  }

  public static Vector3 Direction(this Vector3 original, Vector3 target){
    return original.Heading(target) / original.Distance(target);
  }
}