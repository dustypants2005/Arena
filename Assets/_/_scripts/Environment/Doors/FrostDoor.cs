using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class FrostDoor : MonoBehaviour, IFrostDoor {
    public Animator anim;
    public AudioSource OpenAudio;
    public AudioSource CloseAudio;

    void Awake() {
      if(anim == null){
        Debug.LogError("Anim is null!");
      }
    }

    public void Open() {
      anim.SetBool("isOpen", true);
      CloseAudio.Stop();
      OpenAudio.Play();
    }

    void OnTriggerExit(Collider other) {
      if(other.CompareTag("Player")){
        Close();
      }
    }

    public void Close() {
      if(anim.GetBool("isOpen")){
        anim.SetBool("isOpen", false);
          OpenAudio.Stop();
          CloseAudio.Play();
      }
    }
  }