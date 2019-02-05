using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using dustypants.Managers;

namespace dustypants.UI {
  public class MainMenu : MonoBehaviour {
    public void PlayGame(){
      SceneManager.LoadScene("Level.1.1");
    }

    public void CreateNewGame(){
      SaveManager.instance.data.Reset();
      CoinManager.instance.Reset();
      SaveManager.instance.Save();
      SceneManager.LoadScene("MenuLevel");
    }

    public void ContinueGame(){
      var data = SaveManager.instance.Load();
      SceneManager.LoadScene(data.LevelName);
    }

    public void Quitgame(){
      Application.Quit();
    }

    public void LoadLevel(string levelname){
      SaveManager.instance.data.Spawn = null;
      SceneManager.LoadScene(levelname);
    }
  }
}