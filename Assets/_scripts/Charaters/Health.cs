using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dustypants.AI;
using dustypants.Characters;
using dustypants.Managers;

namespace dustypants {
  [DisallowMultipleComponent]
  public class Health : MonoBehaviour {
    public bool isImmortal = false;
    public float MaxHealth = 100f;
    public Transform Respawn;
    public float RespawnTime = 2f;
    [Tooltip("The speed of the mat change for taking damage.")]
    [SerializeField] private float m_transitionSpeed = .01f;
    [SerializeField] private Image m_healthUI;
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private GameObject HitEffect;
    public float CurrentHealth { get; private set;}
    private bool m_isInvulnerable = false;
    private float startTime;

    private bool isReseting = false;
    void Start () {
      CurrentHealth = MaxHealth;
      startTime = Time.time;
      AdjustHealthBar();
    }

    public void AdjustHealth(float adjustment, bool setInvulnerable = false, float invulnerableTime = 1f){
      if(m_isInvulnerable || isImmortal) return; // if invulnerable, don't take damage
      CurrentHealth += adjustment;
      if(CurrentHealth > MaxHealth) { CurrentHealth = MaxHealth; }
      AdjustHealthBar();
      CheckHP();
      if(!setInvulnerable) return;
      m_isInvulnerable = setInvulnerable;
      if(HitEffect != null){
        Instantiate(HitEffect, transform);// TODO: spawn the hit effect on the location of the hit.
      }
      StartCoroutine(InvulnerableDelay(invulnerableTime)); // end invulnerbality
    }

    void AdjustHealthBar(){
      if(m_healthUI == null) return;
      m_healthUI.fillAmount = CurrentHealth / MaxHealth;
    }

    protected void CheckHP(){
      if(CurrentHealth <= 0 && !isReseting){
        isReseting = true;
        var de =  Instantiate(DeathEffect, transform.position, transform.rotation);
        Destroy(de, 2f); // clean up, remove after use.
        switch(tag){
          case "Player":{
            SimplePlayer.instance.isDisabled = true;
            CoinManager.instance.CoinsCollected = SaveManager.instance.data.Coins;
            CoinManager.instance.UpdateUI();
            StartCoroutine(Reset(true));
            break;
          }
          default:
            var d = GetComponent<Drop>();
            if (d != null) {
              d.Spawn();
            }
            if (Respawn != null) {
              StartCoroutine(Reset(false));
            } else {
              Destroy(gameObject);
            }
            break;
        }
      }
    }

    protected IEnumerator Reset(bool isPlayer){
      yield return new WaitForSeconds(RespawnTime);
      transform.position = Respawn.position;
      transform.Rotate(Respawn.eulerAngles);
      CurrentHealth = MaxHealth;
      isReseting = false;
      AdjustHealthBar();
      if(isPlayer){
        SimplePlayer.instance.isDisabled = false;
        var player = GetComponent<SimplePlayer>();
        player.mouseLook.Rotate(Respawn.rotation);
      }
    }

    protected IEnumerator InvulnerableDelay(float time){
      yield return new WaitForSeconds(time);
      m_isInvulnerable = false;
    }
  }
}
