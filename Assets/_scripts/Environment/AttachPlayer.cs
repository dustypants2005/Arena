using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Environment {
  public class AttachPlayer : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
      if(other.tag == "Player" ){
        other.gameObject.transform.parent = transform;
      }
    }

    private void OnTriggerExit(Collider other) {
      if(other.tag == "Player" ){
        if(other.gameObject.transform.parent == transform) {
          other.gameObject.transform.parent = null;
        }
      }
    }
  }
}