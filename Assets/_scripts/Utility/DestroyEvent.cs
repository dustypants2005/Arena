using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dustypants.Utility {
  public class DestroyEvent : MonoBehaviour {
    public TriggerEvent UnlockEvent;
    void OnDestroy() {
      UnlockEvent.Invoke();
    }

    public void Toggle(){
      UnlockEvent.Invoke();
    }
  }
}
