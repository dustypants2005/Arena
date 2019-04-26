using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.AI {
  public class Turret : Enemy {
    public float IdleDuration = 5;
    public float AttackDuration = 1;
    public float TimeOffset = 0;

    public GameObject ProjectileSpawnObject;
    public bool IsRotating = true;
    [SerializeField]
    public List<Ripple> WaveDirections = new List<Ripple>();
    private bool isChanging = false;

    private void Start() {
      InitWeapon();
      if(ProjectileSpawnObject != null){
        Weapon.ProjectileSpawnObject = ProjectileSpawnObject;
      }
    }
    void Update () {
      if(IsRotating){
        foreach(Ripple wave in WaveDirections){
            RotateTurret(wave.Speed, wave.Strength, wave.Direction);
        }
      }
      if(IdleDuration == 0){
        Attack();
        return;
      }
      Switches();
    }

    public override void Attack(){
      Weapon.Attack();
      if(!isChanging && IdleDuration > 0){
        isChanging = true;
        StartCoroutine(ChangeState(AIstates.Idle, AttackDuration));
      }
    }

    public override void Idle(){
      if(!isChanging){
        isChanging = true;
        StartCoroutine(ChangeState(AIstates.Attack, IdleDuration));
      }
    }

    IEnumerator ChangeState(AIstates newState, float t){
      yield return new WaitForSeconds(t);
      CurrentState = newState;
      isChanging = false;
    }

    private void OnTriggerStay(Collider other) {
      switch(other.tag){
        case "Player":{
          Attack();
          break;
        }
        default:{
          // Debug.Log("tag: " + other.tag);
          break;
        }
      }
    }

    void RotateTurret(float speed, float strength, Vector3 dir){
      var rot = dir * Mathf.Sin((Time.time + TimeOffset)* speed) * strength;
      transform.Rotate(rot);
    }
  }
}
