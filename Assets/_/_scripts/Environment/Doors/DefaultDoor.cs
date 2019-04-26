using UnityEngine;

namespace dustypants.Environment
{
  public class DefaultDoor : MonoBehaviour {
    public bool IsEnabled = true;
    public Animator anim;
    public AudioSource OpenAudio;
    public AudioSource CloseAudio;

    void Awake() {
      if(anim == null){
        Debug.LogError("Anim is null!");
      }
    }

    void OnTriggerEnter(Collider other) {
      if(!IsEnabled) return;
      if(other.CompareTag("Player")){
        anim.SetBool("isOpen", true);
        CloseAudio.Stop();
        OpenAudio.Play();
      }
    }

    void OnTriggerExit(Collider other) {
      if(!IsEnabled) return;
      if(other.CompareTag("Player")){
        anim.SetBool("isOpen", false);
        OpenAudio.Stop();
        CloseAudio.Play();
      }
    }

    public void Enable() {
      IsEnabled = true;
    }

    public void Disable() {
      IsEnabled = false;
    }
  }
}
