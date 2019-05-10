using UnityEngine;

public class Rotate : MonoBehaviour {
  public float Speed = 30f;
  void Update() {
    transform.Rotate(Vector3.up * Speed * Time.deltaTime, Space.World);
  }
}
