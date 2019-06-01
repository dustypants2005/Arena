using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Weapon))]
public class HazardSpawn : MonoBehaviour {
  [SerializeField] private bool isOneShot = true;
  [SerializeField] private float shotduration = 3f;
  private float timer;
  private Weapon weapon;

  private void Awake () {
    weapon = GetComponent<Weapon> ();
  }
  void Start () {
    if (isOneShot) {
      weapon.Attack ();
      return;
    }
    timer = Time.time + shotduration;
  }

  void Update () {
    if (!isOneShot) {
      if (timer > Time.time) {
        weapon.Attack ();
      }
    }
  }
}