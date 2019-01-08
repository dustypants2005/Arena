using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants;
using dustypants.Characters;

namespace dustypants.AI {
  public class AutoTurretAI : Enemy {
    private SimplePlayer target;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float rotationSpeedDamp = 1;
    [SerializeField] private float resetDuration = 1;
    private bool isReseting = false;
    public GameObject ProjectileSpawnObject;
    [SerializeField]
    public List<Ripple> WaveDirections;
    public EnemyAI.PatrolCycle cycle = EnemyAI.PatrolCycle.Linear;

    void Awake() {
      InitWeapon();
      if(ProjectileSpawnObject != null){
        Weapon.ProjectileSpawnObject = ProjectileSpawnObject;
      }
    }

    void Update() {
      if (Scanner != null) {
        target = Scanner.Detect(transform, false);
      } else {
        Debug.LogError("scanner is null");
      }
      if (target == null) {
        CurrentState = AIstates.Patrol;
      } else { // look at player
        CurrentState = AIstates.Chase;
      }
      Switches();
    }
    public override void Idle(){

    }
    public override void Patrol(){
      if(!isReseting){
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
      } else {
        switch(cycle){
          case EnemyAI.PatrolCycle.Linear:{
          transform.rotation = Quaternion.Slerp(transform.rotation,
            new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w),
            (Time.deltaTime * rotationSpeed) / rotationSpeedDamp);
          if(transform.rotation.z <= .1){
            isReseting = false;
          }
            break;
          }
          case EnemyAI.PatrolCycle.Wave: {
            var rot = Vector3.zero;
            foreach(Ripple wave in WaveDirections){
                rot += wave.Direction * Mathf.Sin(Time.time * wave.Speed) * wave.Strength;
            }
            transform.Rotate(rot);
            break;
          }
          default:
            Debug.Log("no cycle");
          break;
        }


      }
    }
    public override void Chase(){
      isReseting = true; // set to true, isReseting only affects turret after loosing target
      var pos = target.transform.position - transform.position;
      // pos.y = 0; // zero out if you want to keep turret horizontal.
      var rot = Quaternion.LookRotation(pos);
      transform.rotation = Quaternion.Slerp(transform.rotation, rot, (Time.deltaTime * rotationSpeed) / rotationSpeedDamp);
      Weapon.Attack();
    }
    public override void Dead(){

    }

    IEnumerator Reset(){
      yield return new WaitForSeconds(resetDuration);
      isReseting = false;
    }
  }
}
