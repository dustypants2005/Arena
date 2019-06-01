using UnityEngine;

public class PlayerWallJump : PlayerJump {

  private void Awake() {
    controller = GetComponent<CharacterController>();
    mover = GetComponent<PlayerMove>();
  }

  public override void Press() {

  }

  public override void Release() {

  }

  private void OnControllerColliderHit(ControllerColliderHit hit) {

  }
}