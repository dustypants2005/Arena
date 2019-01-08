using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants {
  public class Drop : MonoBehaviour {
    public GameObject[] Items;

    private void OnDestroy() {
      foreach(var item in Items){
        Instantiate(item, transform.position, transform.rotation);
      }
    }
  }
}
