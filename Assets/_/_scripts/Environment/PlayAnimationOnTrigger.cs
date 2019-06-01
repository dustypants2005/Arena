using UnityEngine;

public class PlayAnimationOnTrigger : MonoBehaviour
{
  public string AnimationName = "";
  private Animator animator;

  private void Awake() {
    animator = GetComponentInChildren<Animator>();
  }
  private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player"))
    {
      animator.Play(AnimationName, 0, 0);
    }
  }
}