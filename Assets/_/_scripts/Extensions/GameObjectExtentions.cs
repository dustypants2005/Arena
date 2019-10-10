using System;
using UnityEngine;

public static class GameObjectExtentions {
  public static bool NullCheck<T>(this T obj) => obj == null;
}