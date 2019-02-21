using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.AI {
  [RequireComponent(typeof(Rigidbody))]
  public class JumpingAI : Enemy {
    private Rigidbody rb;

    private void Awake() {
      rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
  }
}
