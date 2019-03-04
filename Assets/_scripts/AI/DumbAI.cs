using UnityEngine;

namespace dustypants.AI {
  [RequireComponent(typeof(Rigidbody))]
  public class DumbAI : Enemy {
    private Rigidbody rb;
    [SerializeField] private float speed = 500f;
    // Use this for initialization
    void Start() {
      rb = GetComponent<Rigidbody>();
      CurrentState = AIstates.Patrol;
    }

    // Update is called once per frame
    void Update() {
      Switches();
    }

    public override void Patrol() {
      rb.velocity = Vector3.up * rb.velocity.y;
      rb.AddForce(transform.forward * speed); // forward
      
    }

    private void OnCollisionEnter(Collision collision) {
      if(collision.gameObject.CompareTag("Player")) return;

      foreach(var contact in collision.contacts) {
        if(contact.normal.y < .1f) {
          transform.eulerAngles += Vector3.up * 180;
          rb.velocity = -rb.velocity;
        }
      }
    }
  }
}