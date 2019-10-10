using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour {
  public static ProjectileManager Instance;

  public Dictionary<string, ProjectilePool> Projectiles = new Dictionary<string, ProjectilePool>();

  void Awake() {
    if (Instance == null) {
      DontDestroyOnLoad(gameObject);
      Instance = this;
    } else {
      if (Instance != this) {
        Destroy(gameObject);
      }
    }
  }

  public Projectile GetProjectile(string name) {
    if (!Projectiles.ContainsKey(name)) {
      Add(name);
    }

    ProjectilePool p;
    if (Projectiles.TryGetValue(name, out p)) {
      return p.Get();
    }
    Debug.LogError("Should not return null.");
    return null;
  }

  public void Add(string name) {
    if (Projectiles.ContainsKey(name)) return;
    Projectiles.Add(name, new ProjectilePool());
  }

  public void Return(Projectile pb) {
    ProjectilePool p;
    if (Projectiles.TryGetValue(name, out p)) {
      p.Return(pb);
    }
  }
}