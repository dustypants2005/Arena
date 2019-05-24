using UnityEngine;
namespace dustypants.Characters.Player {
  public class PlayerLook : MonoBehaviour {
    public SimpleMouseLook Look;
    [SerializeField] private Transform cam;
    private PlayerInput input;

    private void Awake() {
      Look.Init(transform, cam);
      input = GetComponent<PlayerInput>();
    }

    private void Update() {
      input.ReadInput();
      Look.LookRotation(transform, cam, input.RS_XAxis, input.RS_YAxis);
    }
  }
}