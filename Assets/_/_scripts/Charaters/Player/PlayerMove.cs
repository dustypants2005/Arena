using UnityEngine;
using dustypants.Characters;

namespace dustypants.Characters.Player {
  [RequireComponent(typeof(CharacterController))]
  public class PlayerMove : Move {
    private CharacterController controller;
    public float Speed = 1f;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake() {
      controller = GetComponent<CharacterController>();
    }

    public void AdditionalMove(Vector3 dir){
      moveDirection += dir;
    }

    public override void Tick(Vector3 dir) {
      controller.Move(
        (transform.TransformDirection(dir.normalized) +
          moveDirection
      )* Speed * Time.deltaTime);
      moveDirection = Vector3.zero; // clear after reading
    }
  }
}
