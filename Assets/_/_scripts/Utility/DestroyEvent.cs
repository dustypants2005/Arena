using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyEvent : MonoBehaviour {
  public TriggerEvent UnlockEvent;
  void OnDestroy () {
    UnlockEvent.Invoke ();
  }

  public void Toggle () {
    UnlockEvent.Invoke ();
  }
}