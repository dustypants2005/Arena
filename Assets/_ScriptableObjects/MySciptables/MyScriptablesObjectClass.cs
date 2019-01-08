using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dustypants {
  [CreateAssetMenu(fileName = "Data", menuName = "Scritabels/PlayerInfo", order = 1)]
  public class MyScriptablesObjectClass : ScriptableObject {
    public string Name = "Player";
  }
}