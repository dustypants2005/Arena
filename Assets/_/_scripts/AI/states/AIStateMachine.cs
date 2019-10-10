using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIStateMachine : MonoBehaviour {
  private Dictionary<Type, IaiState> _availableStates;
  public IaiState CurrentState { get; private set; }
  public void SetState(Dictionary<Type, IaiState> states) {
    _availableStates = states;
  }
  void Start() {
    if (CurrentState == null) {
      CurrentState = _availableStates.Values.First();
    }
  }

  void Update() {
    var nextState = CurrentState.Tick();
    if (nextState != CurrentState.GetType()) {
      SwitchStateTo(nextState);
    }
  }

  public void SwitchStateTo(Type nextState) {
    CurrentState = _availableStates[nextState];
  }
}