using System.Collections;
using System.Collections.Generic;
using dustypants.Characters.Player;
using UnityEngine;

namespace dustypants.AI {

  [RequireComponent(typeof(Rigidbody))]
  public class RigidbodyEnemy : Enemy {
    /// <summary>
    /// GO with children waypoints
    /// </summary>
    public Transform WaypointGroup;
    public float DetectionRadius = 30f;
    public float ChaseRadius = 30f;
    public float IdleDuration = 2f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chaseSpeed = 1f;
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    private Rigidbody rb;
    private SimplePlayer target;
    private bool isIdle = false;
    private List<Transform> Waypoints = new List<Transform>();
    [SerializeField] private int selectedWaypoint = 0;
    [SerializeField] private bool canAttack = false;


    void Awake() {
      rb = GetComponent<Rigidbody>();
      rb.useGravity = false;
      CurrentState = AIstates.Idle;
      Scanner.detectionRadius = DetectionRadius;
      Scanner.maxHeightDifference = DetectionRadius;
      SetWaypoints();
      InitWeapon();
    }

    void Start() {

    }

    void Update() {
      if (Scanner != null && target == null) {
        target = Scanner.Detect(transform);
      } else {
        if(Scanner == null) {
          Debug.LogError("scanner is null");
        }
      }
      if(target != null) {
        var playerHP = target.GetComponent<Health>().CurrentHealth;
        if(playerHP <= 0) {
          CurrentState = AIstates.Idle;
          target = null;
        }
      }
      if((CurrentState != AIstates.Chase && CurrentState != AIstates.Attack) && target != null) {
        CurrentState = AIstates.Chase;
      }
      rb.velocity = Vector3.zero;
      Switches();
    }

    public override void Patrol() {
      isIdle = false;
      RotateToward(Waypoints[selectedWaypoint].position);
      var heading = Waypoints[selectedWaypoint].position - transform.position;
      if (heading.sqrMagnitude >= stoppingDistance * stoppingDistance) { // need to move
        var distance = heading.magnitude;
        var direction = heading / distance;
        rb.AddForce(direction * patrolSpeed);
      } else { // we made it to waypoint, time to idle.
        CurrentState = AIstates.Idle;
      }
    }

    public override void Idle() {
      if (!isIdle) {
        isIdle = true;
        StartCoroutine(DelayNextWaypoint(IdleDuration, AIstates.Patrol));
      }
    }

    public override void Chase() {
      // rotate
      RotateToward(target.transform.position);
      // move
      var heading = target.transform.position - transform.position;
      var distance = heading.magnitude;
      var direction = heading / distance;
      rb.AddForce(direction * chaseSpeed);
      if(heading.sqrMagnitude < ChaseRadius * ChaseRadius 
        && canAttack) { // in attack range
        CurrentState = AIstates.Attack;
      }
    }

    public override void Attack() {
      // rotate
      RotateToward(target.transform.position);

      Weapon.Attack();
      var heading = target.transform.position - transform.position;
      if( heading.sqrMagnitude > ChaseRadius * ChaseRadius) { // out of attack range
        CurrentState = AIstates.Chase;
      }
    }

    public override void Dead() {
      
    }

    IEnumerator DelayNextWaypoint(float time, AIstates newstate) {
      yield return new WaitForSeconds(time);
      CurrentState = newstate;
      NextWaypoint();
    }

    void SetWaypoints() {
      Waypoints.Clear();
      foreach(Transform waypoint in WaypointGroup) {
        Waypoints.Add(waypoint);
      }
    }

    void NextWaypoint() {
      if(selectedWaypoint == Waypoints.Count - 1) {
        selectedWaypoint = 0;
      } else {
        ++selectedWaypoint;
      }
    }

    void RotateToward(Vector3 towards) {
      var pos = towards - transform.position;
      var rot = Quaternion.LookRotation(pos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }
  }
}