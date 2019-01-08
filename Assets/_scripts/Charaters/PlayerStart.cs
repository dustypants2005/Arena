﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants.Managers;
using dustypants.Characters;
using UnityEngine.SceneManagement;

namespace dustypants {
  public class PlayerStart : MonoBehaviour {
    public GameObject PlayerObject;
    void Start () {
      var player = SimplePlayer.instance;
      var playerInfo = SaveManager.instance.data;
      if( player != null ){ // we have player
        if(playerInfo.Spawn == null || playerInfo.LevelName != SceneManager.GetActiveScene().name){
          // no spawn, use this as a spawn
          player.transform.position = transform.position;
          player.mouseLook.Rotate(transform.rotation);
        } else {
          // player with a spawn, then use the spawn info
          player.transform.position = new Vector3( playerInfo.Spawn.Position[0], playerInfo.Spawn.Position[1], playerInfo.Spawn.Position[2]);
          player.mouseLook.Rotate( Quaternion.Euler(new Vector3( playerInfo.Spawn.Rotation[0], playerInfo.Spawn.Rotation[1], playerInfo.Spawn.Rotation[2])));
        }
      } else { // no player, make one
        Instantiate(PlayerObject, transform.position, transform.rotation);
      }
      CoinManager.instance.UpdateUI();
    }
  }
}
