using UnityEngine;

namespace dustypants.Characters.Player {
  public class PlayerMotor {
    public PlayerMotor(PlayerInput _input) {
      input = _input;
    }

    private PlayerInput input;

    public void Tick(){
      input.ReadInput();
    }
  }
}