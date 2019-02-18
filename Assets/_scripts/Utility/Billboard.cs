using UnityEngine;

namespace dustypants.Utility {
  public class Billboard : MonoBehaviour {
      void Update() {
        var heading = Camera.main.transform.position - transform.position;
        transform.LookAt(transform.position - heading);
        transform.rotation = new Quaternion( 0, transform.rotation.y, 0,transform.rotation.w);
      }
  }
}