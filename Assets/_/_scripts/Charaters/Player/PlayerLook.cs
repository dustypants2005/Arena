using UnityEngine;
public class PlayerLook : MonoBehaviour {
  public SimpleMouseLook Look;
  [SerializeField] private Transform cam;
  private SimplePlayerInput input;

  private void Awake() {
    Look.Init(transform, cam);
    input = GetComponent<SimplePlayerInput>();
  }

  private void Update() {
    Look.LookRotation(transform, cam);
  }
}