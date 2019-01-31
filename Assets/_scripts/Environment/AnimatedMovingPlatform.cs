using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AnimatedMovingPlatform : MonoBehaviour {
  public Animator Animator;
  public AudioSource AudioSource;
  public float DelayTimer;

  [SerializeField] private float LiftTime = 0f;
  private float direction;
  private bool isActive = false;
  private bool isPlayingAudio = false;
  private Vector3 oldPosition;

  void Start() {
    oldPosition = transform.position;
  }

  void Update() {
    // MOVE if we are set to move and past the delay timer.
    if(isActive && LiftTime <= Time.time) {
      isActive = false;
      Play();
    }
    // play audio if not playing audio and we are moving.
    if(!isPlayingAudio && oldPosition != transform.position) {
      AudioSource.Play();
      isPlayingAudio = true;
    }
    // stop audio if playing audio and not moving
    if( isPlayingAudio && oldPosition == transform.position) {
      AudioSource.Stop();
      isPlayingAudio = false;
    }
    oldPosition = transform.position;
  }

  void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player")) {
      LiftTime = Time.time + DelayTimer;
      direction = 1;
      isActive = true;
    }
  }

  void OnTriggerExit(Collider other) {
    if(other.CompareTag("Player")) {
      LiftTime = Time.time + DelayTimer;
      direction = -1;
      isActive = true;
    }
  }

  void Play() {
    //Animator.speed = direction;
    Animator.SetFloat("direction",direction);
  }
}
