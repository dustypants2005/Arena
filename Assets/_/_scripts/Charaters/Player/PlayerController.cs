using UnityEngine;

namespace dustypants.Characters.Player {
  public class PlayerController : MonoBehaviour {
    private PlayerInput input;
    private PlayerMove mover;
    private PlayerJump jump;

    private void Awake() {
      input = GetComponent<PlayerInput>();
      mover = GetComponent<PlayerMove>();
      jump = GetComponent<PlayerJump>();
    }

    private void Update() {
      input.ReadInput();
      mover.Tick(new Vector3(input.LS_XAxis, 0, input.LS_YAxis));
      if(input.A_Down) jump.Press();
      if(input.A_Up) jump.Release();
    }
  }
}