using System.Collections;
using System.Collections.Generic;
using dustypants.Utility;
using UnityEngine;


namespace dustypants {
  [RequireComponent(typeof(Collider))]
  public class Activate : MonoBehaviour {
    public bool Value = true;
    public string Target = "Player";
    [SerializeField] ToggleEvent toggle;

    private void OnTriggerEnter(Collider other) {
      if(other.tag == Target)
        toggle.Invoke(Value);
    }

    private void OnTriggerExit(Collider other) {
      if(other.tag == Target)
        toggle.Invoke(!Value);
    }
  }
}
