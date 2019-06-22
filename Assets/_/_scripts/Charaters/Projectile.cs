using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalType {
  None,
  Fire,
  Frost

}

public enum DestroyType {
  Collision,
  Timer
}

public class Projectile : MonoBehaviour {
  public ElementalType Type = ElementalType.None;
  public DestroyType DestroyType = DestroyType.Collision;
  public GameObject impactParticle;
  public GameObject projectileParticle;
  public GameObject muzzleParticle;
  public GameObject[] trailParticles;

  public float Damage { get; set; }
  public float LifeSpan = 5f;
  public float Volume = .001f;

  public bool LowerVolume = true;

  [HideInInspector]
  public Vector3 impactNormal; //Used to rotate impactparticle.

  private bool hasCollided = false;
  private float timestamp;

  void Awake() {
    timestamp = Time.time;
    var audsrc = projectileParticle.GetComponent<AudioSource>();
    if (audsrc != null && LowerVolume) {
      audsrc.volume = Volume;
    }
    projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
    projectileParticle.transform.parent = transform;
    projectileParticle.transform.localScale = transform.localScale;

    if (muzzleParticle) {
      muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
      Destroy(muzzleParticle, 1.5f);
    }
  }

  void Update() {
    // Guarantee the object is destroyed
    if (timestamp + LifeSpan <= Time.time) {
      Debug.Log("Destroy in Projectile");
      Destroy(gameObject);
    }
  }

  void OnCollisionEnter(Collision hit) {
    if (!hasCollided) {
      hasCollided = true;
      // Impact
      if (impactParticle != null) {
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        var audsrc = impactParticle.GetComponent<AudioSource>();
        if (audsrc != null && LowerVolume) {
          audsrc.volume = Volume;
        }
        Destroy(impactParticle, 5f);
      }
      // Projectile will destroy objects tagged as Destructible
      if (hit.gameObject.CompareTag("Destructible")) {
        Destroy(hit.gameObject);
      }
      // Assign Damage
      var enemies = hit.gameObject.GetComponents<Damagable>();
      foreach (var enemy in enemies) {
        enemy.AdjustHealth(-Damage);
      }
      // Trail
      foreach (GameObject trail in trailParticles) {
        GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
        curTrail.transform.parent = null;
        Destroy(curTrail, 3f);
      }
      ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
      //Component at [0] is that of the parent i.e. this object (if there is any)
      for (int i = 1; i < trails.Length; i++) {
        ParticleSystem trail = trails[i];
        if (!trail.gameObject.name.Contains("Trail"))
          continue;

        trail.transform.SetParent(null);
        Destroy(trail.gameObject, 2);
      }
      // Clean up
      if (DestroyType == DestroyType.Collision) {
        Destroy(gameObject);
      }
    }
  }
}