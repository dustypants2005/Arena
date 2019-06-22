using UnityEngine;

public abstract class Move : MonoBehaviour, IMove {
  public Gravity gravity;
  public virtual void Tick(Vector3 dir) { }
  public virtual void AdditionalMove(Vector3 dir) { }
}

public interface IMove {
  void Tick(Vector3 dir);
  void AdditionalMove(Vector3 dir);
}