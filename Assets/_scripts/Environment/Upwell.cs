using dustypants.Characters;
using UnityEngine;

namespace dustypants.Environment
{
  public class Upwell : MonoBehaviour {
    public float speed = .5f;
    private void OnTriggerStay(Collider other) {
      if(other.CompareTag("Player")) {
        var p = SimplePlayer.instance;
        p.AddedverticalVelocity += speed;
      } else {
        var rb = other.GetComponent<Rigidbody>();
        if(rb != null) {
          rb.MovePosition(other.transform.InverseTransformDirection(transform.up) * speed);
        }
      }
    }
  }
}
