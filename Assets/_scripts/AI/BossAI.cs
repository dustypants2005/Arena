using dustypants.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.AI {
  public class BossAI : MonoBehaviour {
    [SerializeField] private TriggerEvent StartEvent;
    [SerializeField] private TriggerEvent EndEvent;
    [SerializeField] private List<BossEvent> HealthEvents;
    [SerializeField] private Health HpRef;
    [SerializeField] private List<string> Animations;

    void Awake() {
      if(HpRef == null) {
        Debug.LogError("No Hp Reference!");
      }
      foreach(var e in HealthEvents) {
        e.HpRef = HpRef;
      }
    }

    void Start() {

    }
    
    void Update() {
      foreach(var e in HealthEvents) {
        e.EventCheck();
      }
    }

    void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player"))
      {
        StartEvent.Invoke();
      }
    }
  }

  [System.Serializable]
  public class BossEvent {
    public string EventName = "Event Name";
    public float TriggerPercentage = .5f;
    public TriggerEvent Event;
    public Health HpRef;
    public bool hasTriggered = false;

    private bool TriggerCheck() {
      if(HpRef.MaxHealth / HpRef.CurrentHealth <= TriggerPercentage) {
        if(!hasTriggered) {
          hasTriggered = true;
          return true;
        }
      }
      return false;
    }

    public void EventCheck() {
      if(TriggerCheck()) {
        Event.Invoke();
      }
    }
  }
}