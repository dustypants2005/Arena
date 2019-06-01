using UnityEngine;

public abstract class Move : MonoBehaviour {
  public Gravity gravity;
  public virtual void Tick(Vector3 dir) { }
  public virtual void AdditionalMove(Vector3 dir) { }
}