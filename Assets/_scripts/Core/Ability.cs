using dustypants.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

namespace dustypants.Core {

  [RequireComponent(typeof(Animator))]
  public abstract class Ability : MonoBehaviour {
    // EFX
    // Name
    // Cooldown
    // UI Element
    public InputAction input;
    /// <summary>
    /// Animation should have same name as Ability.
    /// </summary>
    public Animator Anim;
    public GameObject Efx;
    public float Cooldown = 1f;
    public GameObject UIElement;

    private void OnEnable() {
      input.Enable();
    }

    private void OnDisable() {
      input.Disable();
    }

    void Awake() {
      Anim = GetComponent<Animator>();
      input.performed += ctx => Execute(ctx);
    }

    void Start() {
    }

    /// <summary>
    /// run the ability
    /// </summary>
    public virtual void Execute( InputAction.CallbackContext context) {
      if(Anim != null) {
        Anim.Play(name);
      }
      if(Efx != null) {
        Instantiate(Efx); // Spawn types
      }

      if(UIElement != null) {
        // update UI
      }
    }
  }
}