using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour {
  public float WaitTime = 2f;
  private float time;
  private const string home = "MenuLevel";

  void Start() {
    time = Time.time;
  }

  void Update() {
    if (Input.GetButtonDown("X")) {
      time = Time.time;
      return;
    }
    if (Input.GetButton("X")) {
      if (Time.time > time + WaitTime) {
        SceneManager.LoadScene(home);
        UbhObjectPool.instance.ReleaseAllBullet();
      }
    }
  }
}