using UnityEngine;

public class PlayerJump : Jump {
  public float JumpPower = 1f;
  public bool isJumping { get; private set; }
  protected CharacterController controller;

  private void Awake() {
    controller = GetComponent<CharacterController>();
    mover = GetComponent<PlayerMove>();
  }

  public override void Press() {
    if (controller.isGrounded && !isJumping) { // ground jump
      isJumping = true;
      mover.gravity.Jump(JumpPower);
    }
  }

  public override void Release() {
    isJumping = false;
  }
}