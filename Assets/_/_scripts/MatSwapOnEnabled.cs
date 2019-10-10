using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSwapOnEnabled : MonoBehaviour {
  Renderer rend;
  public Material material;
  void OnEnable() {
    if (rend == null) {
      rend = transform.parent.GetComponentInChildren<Renderer>();
    }
    rend.material = material;
  }
}