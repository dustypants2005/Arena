using System.Collections.Generic;
using UnityEngine;

// Creact an object pool
public abstract class Pool<T> : MonoBehaviour where T : Component {
  public static Pool<T> Instance { get; private set; }
  private Queue<T> objs = new Queue<T>();
  [SerializeField] private T prefab;

  private void Awake() {
    if (Instance == null) {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    } else {
      if (Instance != this) {
        Destroy(gameObject);
      }
    }
  }

  public T Get() {
    if (objs.Count == 0) {
      AddObject();
    }
    return objs.Dequeue();
  }

  public void ReturnToPool(T objToReturn) {
    objToReturn.gameObject.SetActive(false);
    objs.Enqueue(objToReturn);
  }

  public void AddObject() {
    var newobj = GameObject.Instantiate(prefab);
    newobj.gameObject.SetActive(false);
    objs.Enqueue(newobj);
  }
}