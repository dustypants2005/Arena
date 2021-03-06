﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Health))]
public abstract class Enemy : MonoBehaviour {
  public enum AIstates { Idle, Patrol, Chase, Dead, Attack }
  public UbhShotCtrl ShotCtrl;

  public TargetScanner Scanner;
  public AIstates CurrentState;
  private Inventory inventory;

  void Awake() {
    ShotCtrl = GetComponent<UbhShotCtrl>();
  }

  void Start() {
    // InitWeapon();
  }

  void Update() {
    Switches();
  }

  public virtual void Switches() {
    switch (CurrentState) {
      case AIstates.Idle:
        {
          Idle();
          break;
        }
      case AIstates.Patrol:
        {
          Patrol();
          break;
        }
      case AIstates.Chase:
        {
          Chase();
          break;
        }
      case AIstates.Dead:
        {
          Dead();
          break;
        }
      case AIstates.Attack:
        {
          Attack();
          break;
        }
      default:
        Debug.LogError("Should Have State in Enemy script");
        break;
    }
  }
  public virtual void Idle() {

  }
  public virtual void Patrol() {

  }
  public virtual void Chase() {

  }
  public virtual void Dead() {

  }
  public virtual void Attack() { }
}