using UnityEngine;

public class SimplePlayerInput {
  public bool R1 => Input.GetButtonUp("RB");
  public bool L1 => Input.GetButtonUp("LB");
  public bool A_Down => Input.GetButtonDown("A");
  public bool A_Up => Input.GetButtonUp("A");
  public bool A => Input.GetButton("A");
  public bool B_Up => Input.GetButtonUp("B");
  public bool B => Input.GetButton("B");
  public bool B_Down => Input.GetButtonDown("B");
  public bool X => Input.GetButton("X");
  public bool X_Up => Input.GetButtonUp("X");
  public bool X_Down => Input.GetButtonDown("X");
  public bool Y => Input.GetButtonUp("Y");
  public float R2 => Input.GetAxisRaw("TriggersR");
  public float L2 => Input.GetAxisRaw("TriggersL");
  public float LS_XAxis => Input.GetAxisRaw("L_XAxis");
  public float LS_YAxis => Input.GetAxisRaw("L_YAxis");
  public float RS_YAxis => Input.GetAxisRaw("R_YAxis");
  public float RS_XAxis => Input.GetAxisRaw("R_XAxis");
}