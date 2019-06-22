using UnityEngine;

[RequireComponent(typeof(Move))]

public abstract class Jump : MonoBehaviour, IJump {
  protected Move mover;
  public virtual void Press() {

  }

  public virtual void Release() {

  }
}

public interface IJump {
  void Press();

  void Release();
}