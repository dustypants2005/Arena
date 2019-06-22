using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Bomb : MonoBehaviour {
  [Tooltip("How long til boom")]
  public float TimerDuration = 5f;
  public GameObject radius;
  public GameObject ExplosionFX;
  [SerializeField] private const string fresnelPower = "_FresnelPower";

  private Material mat;
  [SerializeField] private float fresnelSpeed = 5f;
  [SerializeField] private float fxDestroyTimer = 5f;
  [Tooltip("Power is set on Start from material.")]
  [SerializeField] private float power = 5f;
  private float timestamp;

  void Awake() {
    mat = radius.GetComponent<Renderer>().material;
    timestamp = Time.time + TimerDuration;
    power = mat.GetFloat(fresnelPower);
  }

  void Update() {
    SignGlow();
    if (timestamp < Time.time) {
      SpawnFX();
      Destroy(gameObject);
    }
  }

  void SignGlow() {
    var signedpower = Mathf.Sin(Time.time * fresnelSpeed) + power;
    mat.SetFloat(fresnelPower, power);
  }

  void SpawnFX() {
    var fx = Instantiate(ExplosionFX, transform.position, transform.rotation);
    Destroy(fx, fxDestroyTimer);
  }
}