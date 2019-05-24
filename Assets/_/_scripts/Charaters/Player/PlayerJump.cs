using UnityEngine;

namespace dustypants.Characters.Player {
  [RequireComponent(typeof(PlayerMove))]
  public class PlayerJump : Jump {
    public float JumpPower = 1f;
    public float Gravity = 30f;

    private float verticalVelocity = -1f;
    public bool isJumping {get; private set;}
    private CharacterController controller;
    private PlayerMove mover;

    private void Awake() {
      controller = GetComponent<CharacterController>();
      mover = GetComponent<PlayerMove>();
    }

    private void Update() {
      isJumping = !FallingCheck();
      ApplyGravity();
    }

    public override void Press(){
      ApplyGravity();
      if(controller.isGrounded) { // ground jump
        isJumping = true;
        verticalVelocity = JumpPower;
      }
      // air jump
      // wall jump

      mover.Tick(new Vector3(0, verticalVelocity,0));
    }

    public override void Release(){
      isJumping = false;
    }

    protected void ApplyGravity(){
      if(!controller.isGrounded){ // add gravity
        verticalVelocity -= isJumping ? Gravity / 2 : Gravity;
      } else {
        if(isJumping) isJumping = false; // landed. change isJumping
        verticalVelocity = -1;
      }
      mover.AdditionalMove(new Vector3(0, verticalVelocity,0));
    }

    // check if we are falling after the jump
    public bool FallingCheck(){
      return controller.velocity.y < 0;
    }
  }
}