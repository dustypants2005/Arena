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
      var player = SimplePlayer.instance.gameObject.transform.position;
      var local = transform.position;
      var lr = (player - local);
      lr.y = 0f;
      var rotation = Quaternion.LookRotation(lr);
      rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, damping)); // TODO: should only rotate on y
      rb.velocity = transform.forward * speed;
    }
  }
}
