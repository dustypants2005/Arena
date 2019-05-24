using UnityEngine;

namespace dustypants.Characters.Player {
  public class PlayerInput: MonoBehaviour {
    public bool R1 {get; private set;}
    public bool L1 {get; private set;}
    public bool A_Down {get; private set;}
    public bool A_Up {get; private set;}
    public bool B {get; private set;}
    public bool X {get; private set;}
    public bool Y {get; private set;}
    public float R2 {get; private set;}
    public float L2 {get; private set;}
    public float LS_XAxis {get; private set;}
    public float LS_YAxis {get; private set;}
    public float RS_YAxis {get; private set;}
    public float RS_XAxis {get; private set;}

    public void ReadInput() {
      R1 = Input.GetButtonUp("RB");
      L1 = Input.GetButtonUp("LB");
      A_Up = Input.GetButtonUp("A");
      A_Down = Input.GetButtonDown("A");
      B = Input.GetButtonUp("B");
      X = Input.GetButtonUp("X");
      Y = Input.GetButtonUp("Y");
      LS_XAxis = Input.GetAxis("L_XAxis");
      LS_YAxis = Input.GetAxis("L_YAxis");
      R2 = Input.GetAxis("TriggersR");
      L2 = Input.GetAxis("TriggersL");
      RS_XAxis = Input.GetAxis("R_XAxis");
      RS_YAxis = Input.GetAxis("R_YAxis");
    }
  }
}