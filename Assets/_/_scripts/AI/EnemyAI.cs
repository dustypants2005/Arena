using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : Enemy {
  public GameObject ProjectileSpawnObject;
  public enum PatrolCycle {
    Linear, // Partol in order from first to last then back to first
    Random, // Choose a random location excluding current target
    Camera, // no movement, only rotation
    Wave,
    None // No movement, no rotation, stationary
  }
  public Transform WaypointGroup;
  [Tooltip("The Object we are rotating up and down for aiming.")]
  public Transform Mount;
  [SerializeField] private bool willChasePlayer = true;
  [Header("Movement")]
  [SerializeField] private PatrolCycle cycle = PatrolCycle.Random;
  [SerializeField] private float walkSpeed = 8;
  [SerializeField] private float runSpeed = 30;
  [SerializeField] private float MinIdleDuration = 1f;
  [SerializeField] private float MaxIdleDuration = 4f;
  [SerializeField] private float defaultStopDistance = 1f;
  [SerializeField] private float chaseStopDistance = 30f;
  [Header("Rotation")]
  [SerializeField] private float rotationSpeed = 1;
  [SerializeField] private float rotationSpeedDamp = 1;
  [Tooltip("The distance we want to detect when to keep this enemy away from other enemies.")]
  // [SerializeField] private float repelRange = 20;
  [SerializeField] private bool isIdling = false;
  [SerializeField] private ToggleEvent ChaseEvent;
  private SimplePlayer target;
  private NavMeshAgent agent;
  [SerializeField] private int selectedWaypoint = 0;
  private List<Transform> Waypoints = new List<Transform>();


    // Use this for initialization
  void Start () {
    agent = GetComponent<NavMeshAgent>();
    agent.autoBraking = true;
    agent.autoRepath = true;
    agent.stoppingDistance = defaultStopDistance;
    SetWaypoints();
    SetDestination();
    InitWeapon();
    if(ProjectileSpawnObject != null){
      Weapon.ProjectileSpawnObject = ProjectileSpawnObject;
    }
  }

  // Update is called once per frame
  void Update () {
    if (Scanner != null) {
      target = Scanner.Detect( transform);
    } else {
      Debug.LogError("scanner is null");
    }
    if(target != null && willChasePlayer){
      ChangeState(AIstates.Chase);
      agent.stoppingDistance = chaseStopDistance;
      ChaseEvent.Invoke(true);
      var pos = target.transform.position - transform.position;
      // pos.y = 0;
      var rot = Quaternion.LookRotation(pos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rot, (Time.deltaTime * rotationSpeed) / rotationSpeedDamp);
    }
    Switches();
  }
  public override void Idle(){
    if(cycle == PatrolCycle.Camera || cycle == PatrolCycle.None){
      return;
    }

    if(!isIdling){
    isIdling = true;
    var time = Random.Range(MinIdleDuration, MaxIdleDuration);
    StartCoroutine(DelayNextWaypoint(time, AIstates.Patrol));
    }
  }

  public override void Patrol(){
    isIdling = false;
    if(agent.remainingDistance <= agent.stoppingDistance){ //TODO: flawed, we need to set the stop distance based on the state. NOT walking around.
      ChangeState(AIstates.Idle);
    }
  }

  public override void Chase(){
    if(target == null) {
      isIdling = false;
      agent.speed = walkSpeed;
      agent.stoppingDistance = defaultStopDistance;
      ChangeState(AIstates.Idle);
      ChaseEvent.Invoke(false);
    }
    // TODO: Fix the repel
    //var enemyGO = GameObject.FindGameObjectsWithTag("Enemy");
    //var repelForce = Vector3.zero;
    //foreach(GameObject enemy in enemyGO){
    //  if(enemy.transform == transform) continue;
    //  if(Vector3.Distance(enemy.transform.position, transform.position) <= repelRange){
    //    repelForce += (transform.position - enemy.transform.position).normalized;
    //  }
    //}
    if(target != null){
      agent.speed = runSpeed;
      // var dir = (transform.position - target.transform.position).normalized;
      // Debug.Log("Dir: "+ dir);
      // Debug.Log("RepelForce: "+ repelForce);

      // TODO:  we need to repel the enemies from one another so they don't stack on top of one another
      if((transform.position - target.transform.position).sqrMagnitude > agent.stoppingDistance * agent.stoppingDistance) {
        agent.SetDestination(target.transform.position);
      } else {// we are close enough to attack. Need to aim toward the player.
        var pos = target.transform.position - transform.position;
        pos.y = 0; // zero out if you want to keep turret horizontal.
        var rot = Quaternion.LookRotation(pos); // Enemy Rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, (Time.deltaTime * rotationSpeed) / rotationSpeedDamp);

        var mountPos = target.transform.position - Mount.position;
        var mountRot = Quaternion.LookRotation(mountPos); // Mount Rotation
        Mount.rotation = Quaternion.Slerp(Mount.rotation, mountRot, (Time.deltaTime * rotationSpeed) / rotationSpeedDamp);

        Weapon.Attack();
      }

    }
  }

  public override void Dead(){
    //TODO: spawn death particles and remove this gameobject
  }

  void ChangeState(AIstates newState ){
    CurrentState = newState;
  }

  void NextWaypoint(){
    switch(cycle){
      case PatrolCycle.Linear:{
        if(selectedWaypoint == Waypoints.Count - 1){
          selectedWaypoint = 0;
        } else {
          ++selectedWaypoint;
        }
        break;
      }
      default:
      break;
    }
    SetDestination();
  }

  void SetDestination(){
    agent.SetDestination(Waypoints[selectedWaypoint].position);
  }

  IEnumerator DelayNextWaypoint(float time, AIstates newstate){
    yield return new WaitForSeconds(time);
    NextWaypoint();
    ChangeState(newstate);
  }
  void SetWaypoints(){
    Waypoints.Clear();
    foreach(Transform waypoint in WaypointGroup){
      Waypoints.Add(waypoint);
    }
  }
}