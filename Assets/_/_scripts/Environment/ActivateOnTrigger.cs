using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour {
  [SerializeField] private bool isActivated = false;
  [SerializeField] private float resetTimer = 5f;
  [SerializeField] private string playerTag = "Player";
  void Start() {

  }

  void Update() {

  }

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag(playerTag)) {
      // TODO: finish or remove this, moving platform may be able to replace this
    }
  }
}