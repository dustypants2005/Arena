using UnityEngine;

public class Upwell : MonoBehaviour {
  public float speed = .5f;
  private void OnTriggerStay(Collider other) {
    if(other.CompareTag("Player")) {
      var p = SimplePlayer.instance;
      p.AddedverticalVelocity += speed;
    }
  }
}