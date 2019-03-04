using dustypants.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.AI {
  [RequireComponent(typeof(Rigidbody))]
  public class JumpingAI : Enemy {
    private Rigidbody rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpTimer = 5f;
    private float timer;

    private void Awake() {
      rb = GetComponent<Rigidbody>();
      timer = Time.time + jumpTimer;
    }

    void Start() {
      CurrentState = AIstates.Patrol;
    }

    void Update() {
      Switches();
    }

    public override void Patrol() {
      // jump on interval
      if(timer < Time.time && GroundCheck()) {
        rb.AddForce(Vector3.up * jumpHeight); // jump
        rb.AddForce(transform.forward * speed); // forward
        timer = Time.time + jumpTimer; // reset timer
      }
    }

    private void OnCollisionEnter(Collision collision) {
      if(collision.gameObject.CompareTag("Player")) return;

      foreach(var contact in collision.contacts) {
        if(contact.normal.y < .1f) {
          transform.eulerAngles += Vector3.up * 180;
          rb.velocity = -rb.velocity;
        }
      }
    }

    bool GroundCheck() {
      RaycastHit hit;
      float distance = 1f;
      var dir = Vector3.down;
      if(Physics.Raycast(transform.position, dir, out hit, distance)) {
        return true;
      }
      return false;
    }
  }
}
