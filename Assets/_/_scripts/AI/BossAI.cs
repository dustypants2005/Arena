using dustypants.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants.AI {
  [RequireComponent(typeof(Animator))]
  public class BossAI : MonoBehaviour {
    [SerializeField] private TriggerEvent StartEvent;
    [SerializeField] private TriggerEvent EndEvent;
    [SerializeField] private Health HpRef;
    [SerializeField] private List<BossEvent> HealthEvents;
    private Animator anim;
    private bool hasStarted = false;

    void Awake() {
      anim = GetComponent<Animator>();
      if(HpRef == null) {
        Debug.LogError("No Hp Reference!");
      }
      foreach(var e in HealthEvents) {
        e.SetHpRef(HpRef);
      }
    }

    void Start() {
    }

    void Update() {
      foreach(var e in HealthEvents) {
        if(e.hasTriggered) continue;
        e.EventCheck(anim);
      }
    }

    void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player") && !hasStarted) {
        hasStarted = true;
        StartEvent.Invoke();
        anim.SetBool("IsActive", true);
        }
    }

    void OnDestroy() {
      EndEvent.Invoke();
    }
  }

  [System.Serializable]
  public class BossEvent {
    public string EventName = "Event Name";
    public bool hasTriggered = false;
    public int Stage = 0;
    [SerializeField] private float TriggerPercentage = .5f;
    [SerializeField] private TriggerEvent Event;
    private Health HpRef;

    private const string stageString = "Stage";

    /// <summary>
    /// should we run the event?
    /// </summary>
    /// <returns></returns>
    private bool TriggerCheck() {
      var c = HpRef.CurrentHealth / HpRef.MaxHealth;
      if( c <= TriggerPercentage) {
        hasTriggered = true;
        return true;
      }
      return false;
    }

    /// <summary>
    /// if TriggerCheck then invoke event.
    /// </summary>
    public void EventCheck(Animator anim) {
      if(TriggerCheck()) {
        Event.Invoke();
        anim.SetInteger( stageString, Stage);
      }
    }

    public void SetHpRef(Health h) {
      HpRef = h;
    }
  }
}