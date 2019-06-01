using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

  public TriggerEvent OnEvent;
  public TriggerEvent OffEvent;

  private void Awake() {
  }

  private void OnTriggerEnter(Collider other) {
    if(other.tag == "Player"){
      var hp = other.GetComponent<Health>();
      if(hp == null) { return; }
      if(hp.Respawn != null){
        var previousCheckPoint = hp.Respawn.GetComponent<CheckPoint>();
        if(previousCheckPoint == this) return;
        previousCheckPoint.OffEvent.Invoke();
      }
      hp.Respawn = transform;
      OnEvent.Invoke();
      SaveManager.instance.SavePlayer(transform);
    }
  }
}