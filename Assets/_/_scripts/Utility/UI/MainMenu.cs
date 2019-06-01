using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  public void PlayGame () {
    SceneManager.LoadScene ("Level.1.1");
  }

  public void CreateNewGame () {
    SaveManager.instance.Reset ();
    CoinManager.instance.Reset ();
    WeaponsManager.instance.Reset ();
    SceneManager.LoadScene ("MenuLevel");
  }

  public void ContinueGame () {
    var data = SaveManager.instance.LoadPlayerInfo ();
    SceneManager.LoadScene (data.LevelName);
  }

  public void Quitgame () {
    Application.Quit ();
  }

  public void LoadLevel (string levelname) {
    SaveManager.instance.data.Spawn = null;
    SceneManager.LoadScene (levelname);
  }
}