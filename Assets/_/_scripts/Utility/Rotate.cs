using UnityEngine;

public class Rotate : MonoBehaviour {
  public Vector3 direction = Vector3.up;
  public float Speed = 30f;
  void Update() {
    transform.Rotate(direction * Speed * Time.deltaTime, Space.World);
  }
}