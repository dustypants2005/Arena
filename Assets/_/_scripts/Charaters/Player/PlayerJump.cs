using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerJump : Jump {
  public float JumpPower = 1f;
  public bool isJumping { get; protected set; }
  public GameObject JumpFX;
  protected CharacterController controller;

  private void Awake() {
    controller = GetComponent<CharacterController>();
    mover = GetComponent<PlayerMove>();
  }

  public override void Press() {
    if (controller.isGrounded && !isJumping) { // ground jump
      isJumping = true;
      mover.gravity.Jump(JumpPower);
      mover.gravity.UpdateVeloctiy(isJumping);
      if (JumpFX != null) {
        Instantiate(JumpFX, transform.position, transform.rotation);
      }
    }
  }

  public override void Release() {
    isJumping = false;
    mover.gravity.UpdateVeloctiy(isJumping);
  }
}