using UnityEngine;

public class Drop : MonoBehaviour {
  public GameObject[] Items;

  public void Spawn() {
    foreach(var item in Items){
      Instantiate(item, transform.position, transform.rotation);
    }
  }
}