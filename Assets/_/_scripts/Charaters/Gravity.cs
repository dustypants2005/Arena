using System;
using UnityEngine;

[Serializable]
public class Gravity {
  public float Value = .1f;
  public float DividedBy = 2f;
  public float GroundedGravity = -1f;
  public float VerticalVelocity;

  public void UpdateVeloctiy(bool isJumping) {
    VerticalVelocity -= isJumping ? Value / DividedBy : Value;
  }

  public void ResetVelocity() {
    VerticalVelocity = GroundedGravity;
  }

  public Vector3 GetVectorVeloctiy() {
    return new Vector3(0, VerticalVelocity, 0);
  }

  // jump sets the vertical velocity
  public void Jump(float amount) {
    VerticalVelocity = amount;
  }
}