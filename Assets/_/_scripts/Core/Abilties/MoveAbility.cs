using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Input;
using dustypants.Utility;

namespace dustypants.Core.Abilities{
  [RequireComponent(typeof(CharacterController))]
  public class MoveAbility : Ability {
    private CharacterController controller;
    public TriggerEvent triggerEvent;
    public float Speed = 1f;

    private void Awake() {
      controller = GetComponent<CharacterController>();
      input.performed += ctx => Execute(ctx);
    }

    public void Move(InputAction.CallbackContext context) {
      var dir = context.ReadValue<Vector2>();
      controller.Move(new Vector3(dir.x, 0, dir.y).normalized * Speed);
    }



    public override void Execute(InputAction.CallbackContext context) {
      Move(context);
      base.Execute(context);
    }
  }
}