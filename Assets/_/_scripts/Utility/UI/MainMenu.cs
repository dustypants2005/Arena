using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  public void PlayGame() {
    SceneManager.LoadScene("Level.1.1");
  }

  public void CreateNewGame() {
    // SaveManager.instance.Reset();
    // CoinManager.instance.Reset();
    SaveManager.Save(SaveManager.FileName, new PlayerInfo());
    SaveManager.Save(CoinManager.FileName, new Dictionary<string, Dictionary<float, bool>>());
    SaveManager.instance.LoadPlayerInfo();
    SceneManager.LoadScene("MenuLevel");
  }

  public void ContinueGame() {
    var data = SaveManager.instance.LoadPlayerInfo();
    SceneManager.LoadScene(data.LevelName);
  }

  public void Quitgame() {
    Application.Quit();
  }

  public void LoadLevel(string levelname) {
    SaveManager.instance.data.Spawn = null;
    SceneManager.LoadScene(levelname);
  }
}