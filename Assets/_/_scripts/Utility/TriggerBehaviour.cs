using UnityEngine;

namespace dustypants.Utility {
  public class TriggerBehaviour : MonoBehaviour {
     public TriggerEvent enter;
     public TriggerEvent exit;
     public TriggerEvent stay;

     private void OnTriggerEnter(Collider other) {
       if(other.tag == "Player"){
         enter.Invoke();
       }
     }

     private void OnTriggerExit(Collider other) {
       if(other.tag == "Player"){
         exit.Invoke();
       }
     }

     private void OnTriggerStay(Collider other) {
       if(other.tag == "Player"){
         stay.Invoke();
       }
     }
  }
}