using UnityEngine;
public class PlayerLook : MonoBehaviour {
  public SimpleMouseLook Look;
  [SerializeField] private Transform cam;
  private PlayerInput input;

  private void Awake() {
    Look.Init(transform, cam);
    input = GetComponent<PlayerInput>();
  }

  private void Update() {
    Look.LookRotation(transform, cam);
  }
}
