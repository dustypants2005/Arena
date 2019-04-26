using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Environment {
  [DisallowMultipleComponent]
  public class TransparentMat : MonoBehaviour {
    public Material transparentMat;
    private Material defaultMat;
    private Renderer rend;

    private void Start() {
      rend = GetComponent<Renderer>();
      defaultMat = rend.material;
    }

    public void ApplyDefaultMat(){
      rend.material = defaultMat;
    }
    public void ApplyTransparentMat(){
      rend.material = transparentMat;
    }

  }
}
