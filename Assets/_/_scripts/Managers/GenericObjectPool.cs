using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component {
  [SerializeField] private T prefab;

  public static GenericObjectPool<T> Instance { get; private set; }
  private Queue<T> objects = new Queue<T>();

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
    if (objects.Count == 0) {
      AddObject();
    }
    return objects.Dequeue();
  }

  public void Return(T obj) {
    obj.gameObject.SetActive(false);
    objects.Enqueue(obj);
  }

  private void AddObject() {
    var newobj = GameObject.Instantiate(prefab);
    newobj.gameObject.SetActive(false);
    objects.Enqueue(newobj);
  }
}