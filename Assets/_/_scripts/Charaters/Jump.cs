using UnityEngine;

[RequireComponent(typeof(Move))]

public abstract class Jump : MonoBehaviour {
  protected Move mover;
  public virtual void Press() {

  }

  public virtual void Release() {

  }
}