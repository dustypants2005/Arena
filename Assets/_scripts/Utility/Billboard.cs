using System.Collections;
using System.Collections.Generic;
using dustypants.Characters;
using UnityEngine;

namespace dustypants.Utility {
  public class Billboard : MonoBehaviour {
      void Update() {
        var heading = SimplePlayer.instance.transform.position - transform.position;
        transform.LookAt(transform.position - heading);
      }
  }
}