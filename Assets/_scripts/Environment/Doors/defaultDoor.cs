using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Environment {
  public class defaultDoor : MonoBehaviour {
    public Animator anim;
    public AudioSource OpenAudio;
    public AudioSource CloseAudio;

    void Awake() {
      if(anim == null){
        Debug.LogError("Anim is null!");
      }
    }

    void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        anim.SetBool("isOpen", true);
        CloseAudio.Stop();
        OpenAudio.Play();
      }
    }

    void OnTriggerExit(Collider other) {
      if(other.CompareTag("Player")){
        anim.SetBool("isOpen", false);
        OpenAudio.Stop();
        CloseAudio.Play();
      }
    }
  }
}
