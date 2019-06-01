using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerEnd : MonoBehaviour {
  public string NextLevelName = "Menu-Main";

  void OnTriggerEnter(Collider other) {
    switch(other.tag){
      case "Player":{
        SaveManager.instance.data.Spawn = null;
        SceneManager.LoadScene(NextLevelName);
        break;
      }
      default:{
      break;
      }
    }
  }
}
