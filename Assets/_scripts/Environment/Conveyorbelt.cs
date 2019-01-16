using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants.Characters;

namespace dustypants.Environment {
  public class Conveyorbelt : MonoBehaviour {
    public float speed = .5f;
    private void OnTriggerStay(Collider other) {
      if(other.CompareTag("Player")){
        var p = SimplePlayer.instance;
        p.moveDirection += p.transform.InverseTransformDirection(transform.forward) * speed;
      }
    }
}
}
