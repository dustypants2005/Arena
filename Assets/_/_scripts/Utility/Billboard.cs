using UnityEngine;

public class Billboard : MonoBehaviour {
  void Update() {
    var cam = Camera.main;
    if (cam == null) { return; }
    var heading = cam.transform.position - transform.position;
    transform.LookAt(transform.position - heading);
    // transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
  }
}