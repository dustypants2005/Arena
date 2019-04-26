using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectInput : MonoBehaviour {
  public EventSystem eventSystem;
  public GameObject selectedObject;

  private bool buttonSelected;

  void Start() {
    eventSystem.SetSelectedGameObject(selectedObject);
  }

  void Update() {
    if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false) {
      eventSystem.SetSelectedGameObject(selectedObject);
      buttonSelected = true;
    }
  }

  private void OnDisable() {
   buttonSelected = false;
  }

  private void OnEnable() {
    eventSystem.SetSelectedGameObject(selectedObject);
    buttonSelected = true;
  }
}
