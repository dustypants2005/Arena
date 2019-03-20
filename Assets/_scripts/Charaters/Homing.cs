using dustypants.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Characters {
  public class Homing : MonoBehaviour {
    public float damping = .5f;
    public float speed = 1;
    void Update () {
      var rb = transform.GetComponent<Rigidbody>();
      var rotation = Quaternion.LookRotation(SimplePlayer.instance.gameObject.transform.position - transform.position);
      rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, damping));
      rb.velocity = transform.forward * speed;
    }
  }
}
