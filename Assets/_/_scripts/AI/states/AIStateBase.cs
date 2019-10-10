using System;
using UnityEngine;

public abstract class AIStateBase : IaiState {

  public AIStateBase(GameObject go) {
    gameobject = go;
    transform = go.transform;
  }
  protected GameObject gameobject;
  protected Transform transform;

  public abstract Type Tick();
}