using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using dustypants.Characters;

namespace dustypants.Managers{
  public class CoinManager : MonoBehaviour {
    public static CoinManager instance;
    public int Coins = 0;

    private void Awake() {
      if(instance == null){
        DontDestroyOnLoad(gameObject);
        instance = this;
      } else {
        if(instance != this){
          Destroy(gameObject);
        }
      }
    }
    private void Start() {
      UpdateUI();
    }

    public void Add(int amount){
      Coins += amount;
      UpdateUI();
    }

    public void Subtract(int amount){
      Coins -= amount;
      if(Coins < 0) Coins = 0;
      UpdateUI();
    }

    public void Reset(){
      Coins = 0;
      UpdateUI();
    }

    public void UpdateUI(){
      if(SimplePlayer.instance != null){
        SimplePlayer.instance.UpdateCoins(Coins);
      }
    }
  }
}
