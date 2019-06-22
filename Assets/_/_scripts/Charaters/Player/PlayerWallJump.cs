using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerWallJump : PlayerJump {
  private PlayerStats stats;
  [SerializeField] private float wallJumpPower = 1f;
  private Vector3 wallJumpDir = Vector3.zero;
  private bool canJump = false;

  private void Awake() {
    controller = GetComponent<CharacterController>();
    mover = GetComponent<PlayerMove>();
    stats = GetComponent<PlayerStats>();
  }

  private void Update() {
    if (controller.velocity.y < 0 || controller.isGrounded) {
      isJumping = false;
      canJump = false;
    }
  }

  public override void Press() {
    if (!controller.isGrounded) {
      canJump = true;
      Debug.Log("Pressed!");
    }
  }

  public override void Release() {
    canJump = false;
    isJumping = false;
  }

  private void OnControllerColliderHit(ControllerColliderHit hit) {
    var info = stats.Info;
    if (!controller.isGrounded && canJump && !isJumping) {
      if (hit.normal.y < .1f) {
        isJumping = true;
        mover.gravity.Jump(JumpPower);
        // TODO: this is not pushing away from wall
        mover.AdditionalMove(hit.normal * wallJumpPower);
      }
    }
  }
}