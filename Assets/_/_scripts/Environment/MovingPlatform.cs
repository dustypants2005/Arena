﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

  public Transform Waypoints;
  public float WaitTime = 2;
  public float MoveSpeed = 2;
  public float ReturnMoveSpeed = 2;
  public bool UseReturnSpeed = true;
  public float MoveOffset = 0;
  private Transform[] m_TransformList;
  public Color gizmoColor;
  [Tooltip("Circular: waypoints first to last then back to start. Linear: start to finish on repeat. Random: random waypoint chosen.")]
  public Cycles SelectedCyle = Cycles.Circular;
  public enum Cycles { Linear, Circular, Trigger, Random }

  [SerializeField] private bool m_active = true;
  [SerializeField] private bool m_isLooping = true;
  private int m_currentTransform;
  private bool isPlayerAttached = false;
  float enteredTime = 0f;

  void Start() {
    m_TransformList = new Transform[Waypoints.childCount];
    var i = 0;
    foreach (Transform child in Waypoints) {
      m_TransformList[i++] = child;
    }
  }

  IEnumerator Idle() {
    if (MoveOffset > 0) {
      yield return new WaitForSeconds(WaitTime + MoveOffset);
      MoveOffset = 0;
      SetLoop(true);
    } else {
      yield return new WaitForSeconds(WaitTime);
      SetLoop(true);
    }
  }

  void FixedUpdate() {
    if (!m_active) return;

    if (m_TransformList.Length == m_currentTransform) {
      m_currentTransform = 0;
    }
    if (m_isLooping) {
      if (transform.position.Distance(m_TransformList[m_currentTransform].position) <= 0.1f) {
        switch (SelectedCyle) {
          case Cycles.Circular:
            SetLoop(false);
            m_currentTransform++;
            StartCoroutine(Idle());
            return;
          case Cycles.Linear:
            m_currentTransform = 1; // always go to "last" waypoint. TODO: make sure there is only 2 waypoints for linear
            transform.position = m_TransformList[0].position; // reset to first waypoint
            StartCoroutine(Idle());
            break;
          case Cycles.Trigger:
            if (isPlayerAttached) {
              if (Time.time > enteredTime) {
                m_currentTransform = 0; // waited in the trigger for the lift
              } else {
                m_currentTransform = 1;
              }
            } else {
              m_currentTransform = 0;
            }
            break;
          default:
            Debug.LogError("Should have a Cycle selected!");
            break;
        }
      }
      var dist = transform.position.Distance(m_TransformList[m_currentTransform].position);
      transform.position = Vector3.Lerp(transform.position, m_TransformList[m_currentTransform].position, (GetMoveSpeed() / dist) / 10);
    }
  }

  float GetMoveSpeed() {
    return UseReturnSpeed && m_currentTransform == 0 ?
      ReturnMoveSpeed :
      MoveSpeed;
  }

  void SetLoop(bool value) {
    m_isLooping = value;
  }

  void OnDrawGizmos() {
    Gizmos.color = gizmoColor;
    foreach (Transform child in Waypoints) {
      Gizmos.DrawWireCube(child.position, child.localScale);
    }

  }

  public void Activate() {
    m_active = true;
  }

  public void Deactivate() {
    m_active = false;
  }

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      isPlayerAttached = true;
      enteredTime = Time.time + WaitTime + MoveSpeed;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      isPlayerAttached = false;
    }
  }
}