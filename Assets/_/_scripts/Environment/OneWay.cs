using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.Environment {
  public class OneWay : MonoBehaviour {
    [SerializeField] private Collider m_col;
    private void OnTriggerEnter(Collider other) {
      if(other.tag == "Player")
        m_col.enabled = false;
    }
    private void OnTriggerExit(Collider other) {
      if(other.tag == "Player")
        m_col.enabled = true;
    }
  }
}