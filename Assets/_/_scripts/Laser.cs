using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class Laser : MonoBehaviour {
  public float MaxDistance = 100f;
  public float Damage = 10f;
  public float DamageInterval = 3f;
  RaycastHit hit;
  LineRenderer line;
  int layer;

  float nextDamageTime { get; set; }

  void Awake() {
    line = GetComponent<LineRenderer>();
    layer = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
    SetDamageTime();
  }

  void Update() {
    if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance, layer, QueryTriggerInteraction.Ignore)) {
      // we hit something
      line.SetPosition(1, Vector3.forward * hit.distance);
      var hp = hit.transform.GetComponent<Damageable>();
      if (!hp.NullCheck() && Time.time > nextDamageTime) {
        hp.AdjustHealth(-Damage);
        SetDamageTime();
      }

    } else {
      // we hit NOTHING
      line.SetPosition(1, Vector3.forward * MaxDistance);
    }
  }

  void SetDamageTime() {
    nextDamageTime = Time.time + DamageInterval;
  }
}