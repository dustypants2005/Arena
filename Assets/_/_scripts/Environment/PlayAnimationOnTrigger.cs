using UnityEngine;

public class PlayAnimationOnTrigger : MonoBehaviour {
  public string AnimationName = "";
  private Animator animator;
  private float playedTimer;
  [SerializeField] private float resetTimer = 5f;

  private void Awake() {
    animator = GetComponentInChildren<Animator>();
  }
  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player") && playedTimer < Time.time) {
      playedTimer = Time.time + resetTimer;
      animator.Play(AnimationName, 0, 0);
    }
  }
}