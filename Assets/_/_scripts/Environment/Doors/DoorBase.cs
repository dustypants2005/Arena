using UnityEngine;

public abstract class DoorBase : MonoBehaviour, IDoor {
  public Animator anim;
  public AudioSource OpenAudio;
  public AudioSource CloseAudio;
  public ElementalType type = ElementalType.None;
  void Awake() {
    if (anim == null) {
      Debug.LogError("Anim is null!");
    }
  }

  public virtual void Open() {
    anim.SetBool("isOpen", true);
    CloseAudio.Stop();
    OpenAudio.Play();
  }

  public virtual void Close() {
    if (anim.GetBool("isOpen")) {
      anim.SetBool("isOpen", false);
      OpenAudio.Stop();
      CloseAudio.Play();
    }
  }

  void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      Close();
    }
  }

  void OnCollisionEnter(Collision other) {
    var projectile = other.gameObject.GetComponentInParent<Projectile>();
    if (projectile != null) {
      if (projectile.Type == type) Open();
    }
  }
}