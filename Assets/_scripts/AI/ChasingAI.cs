using UnityEngine;
using System.Collections;
using dustypants.Characters;

namespace dustypants.AI {
  [RequireComponent(typeof(Rigidbody))]
  public class ChasingAI : Enemy {
    [SerializeField] private float speed = 500f;
    private Rigidbody rb;
    private SimplePlayer target;
    public Transform WaypointGroup;
    public float DetectionRadius = 30f;
    public float ChaseRadius = 30f;
    public float IdleDuration = 2f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chaseSpeed = 1f;
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    private bool isIdle = false;
    [SerializeField] private int selectedWaypoint = 0;
    [SerializeField] private bool canAttack = false;

    void Awake() {
      rb.GetComponent<Rigidbody>();
      CurrentState = AIstates.Patrol;
    }

    void Start() {

    }

    void Update() {
      if(target == null) {
        target = Scanner.Detect(transform);
      }
      if(target != null && CurrentState != AIstates.Chase) {
        CurrentState = AIstates.Chase;
      }

      Switches();
    }

    public override void Chase() {
      // rotate
      RotateToward(target.transform.position);
      // move
      var heading = target.transform.position - transform.position;
      var distance = heading.magnitude;
      var direction = heading / distance;
      rb.AddForce(direction * chaseSpeed);
      if(heading.sqrMagnitude < ChaseRadius * ChaseRadius) { // in attack range
        CurrentState = AIstates.Attack;
      }
    }

    public override void Patrol() {
      rb.velocity = Vector3.up * rb.velocity.y; // reset velocity except Y axis. Must fall.
      rb.AddForce(transform.forward * speed); // forward
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

    void RotateToward(Vector3 towards) {
      var pos = towards - transform.position;
      var rot = Quaternion.LookRotation(pos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }
  }
}