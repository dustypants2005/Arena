using UnityEngine;

public class PlayerInput: MonoBehaviour {
  public bool R1 => Input.GetButtonUp("RB");
  public bool L1 => Input.GetButtonUp("LB");
  public bool A_Down => Input.GetButtonDown("A");
  public bool A_Up => Input.GetButtonUp("A");
  public bool A => Input.GetButton("A");
  public bool B => Input.GetButtonUp("B");
  public bool X => Input.GetButtonUp("X");
  public bool Y => Input.GetButtonUp("Y");
  public float R2 => Input.GetAxis("TriggersR");
  public float L2 => Input.GetAxis("TriggersL");
  public float LS_XAxis => Input.GetAxis("L_XAxis");
  public float LS_YAxis => Input.GetAxis("L_YAxis");
  public float RS_YAxis => Input.GetAxis("R_YAxis");
  public float RS_XAxis => Input.GetAxis("R_XAxis");
}