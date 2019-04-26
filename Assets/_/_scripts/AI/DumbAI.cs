using UnityEngine;

namespace dustypants.AI {
  [RequireComponent(typeof(Rigidbody))]
  public class DumbAI : Enemy {
    private Rigidbody rb;
    [SerializeField] private float speed = 500f;
    [SerializeField] private float RayDistance = 1.5f;
    

    void Start() {
      rb = GetComponent<Rigidbody>();
      CurrentState = AIstates.Patrol;
    }

    void Update() {
      Switches();
    }

    void FixedUpdate() {
      RaycastHit hit;
      if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance)) {
        if (!hit.transform.gameObject.CompareTag("Player")) {
          TurnAround();
        }
      }
    }

    public override void Patrol() {
      rb.velocity = Vector3.up * rb.velocity.y; // reset velocity except Y axis. Must fall.
      rb.AddForce(transform.forward * speed); // forward
    }

    void TurnAround() {
      transform.eulerAngles += Vector3.up * 180;
      rb.velocity = -rb.velocity;
    }
  }
}