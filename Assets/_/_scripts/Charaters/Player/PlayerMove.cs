using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : Move {
  public bool A { get; set; }
  private CharacterController controller;
  public float Speed = 1f;
  private Vector3 moveDirection = Vector3.zero;

  private void Awake() {
    controller = GetComponent<CharacterController>();
  }

  public override void AdditionalMove(Vector3 dir) {
    moveDirection += dir;
  }

  public override void Tick(Vector3 dir) {
    controller.Move(
      (transform.TransformDirection(dir.normalized) +
        moveDirection + gravity.GetVectorVeloctiy()
      ) * Speed * Time.deltaTime);
    moveDirection = Vector3.zero; // clear after reading
    if (controller.isGrounded) {
      gravity.ResetVelocity();
    } else {
      gravity.UpdateVeloctiy(A && controller.velocity.y >= 0);
    }
  }

}