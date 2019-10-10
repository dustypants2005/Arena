using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SimplePlayerInput))]
[RequireComponent(typeof(PlayerMove))]
public class PlayerController : MonoBehaviour {
  private SimplePlayerInput input;
  private PlayerMove mover;
  private UnityEvent pressEvents = new UnityEvent();
  private UnityEvent releaseEvents = new UnityEvent();

  private void Awake() {
    input = GetComponent<SimplePlayerInput>();
    mover = GetComponent<PlayerMove>();
    foreach (var jump in GetComponents<Jump>()) {
      pressEvents.AddListener(jump.Press);
      releaseEvents.AddListener(jump.Release);
    }
  }

  private void Update() {
    mover.A = input.A;
    mover.Tick(new Vector3(input.LS_XAxis, 0, input.LS_YAxis));
    if (input.A_Down) pressEvents.Invoke();
    if (input.A_Up) releaseEvents.Invoke();
  }
}