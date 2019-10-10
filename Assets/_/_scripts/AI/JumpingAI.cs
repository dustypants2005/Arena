using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpingAI : Enemy {
  private Rigidbody rb;
  [SerializeField] private float speed = 1f;
  [SerializeField] private float jumpHeight = 1f;
  [SerializeField] private float jumpTimer = 5f;
  [SerializeField] private float RayDistance = 1.5f;
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

  private void FixedUpdate() {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)) {
      if (!hit.transform.gameObject.CompareTag("Player")) {
        TurnAround();
      }
    }
  }

  public override void Patrol() {
    // jump on interval
    var gc = GroundCheck();
    if (gc) {
      rb.velocity = Vector3.zero;
      if (timer < Time.time) {
        rb.AddForce(Vector3.up * jumpHeight); // jump
        rb.AddForce(transform.forward * speed); // forward
        timer = Time.time + jumpTimer; // reset timer
      }
    }
  }

  bool GroundCheck() {
    RaycastHit hit;
    float distance = 1f;
    var dir = Vector3.down;
    if (Physics.Raycast(transform.position, dir, out hit, distance)) {
      return true;
    }
    return false;
  }

  void TurnAround() {
    transform.eulerAngles += Vector3.up * 180;
    rb.velocity = -rb.velocity;
  }
}