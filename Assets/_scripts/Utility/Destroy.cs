using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Utility {
  public class Destroy : MonoBehaviour {
    public float lifetime = 5f;
    void Start() {
      Destroy(gameObject, lifetime);
    }
  }
}