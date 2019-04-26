using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants.Environment;

namespace dustypants {
  public enum ElementalType {
    None,
    Fire,
    Frost

  }

  public class Projectile : MonoBehaviour {
    public string IgnoreTag = "Player";
    public ElementalType Type = ElementalType.None;
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
    public string TagToDamage = "Enemy";

    public float Damage { get; set; }
    public float Volume = .001f;

    public bool LowerVolume = true;

    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false;

    void Awake(){
      var audsrc = projectileParticle.GetComponent<AudioSource>();
      if(audsrc != null && LowerVolume){
        audsrc.volume = Volume;
      }
      projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
      projectileParticle.transform.parent = transform;
      projectileParticle.transform.localScale = transform.localScale;

      if (muzzleParticle)
      {
        muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
        Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
      }
    }

    void OnCollisionEnter(Collision hit) {
      if (!hasCollided) {
        hasCollided = true;
        //transform.DetachChildren();
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        var audsrc = impactParticle.GetComponent<AudioSource>();
        if(audsrc != null && LowerVolume){
          audsrc.volume = Volume;
        }
        if (hit.gameObject.CompareTag("Destructible")){// Projectile will destroy objects tagged as Destructible
          Destroy(hit.gameObject);
        }

        var enemies = hit.gameObject.GetComponents<Damagable>();
        foreach(var enemy in enemies) {
          if(enemy != null && enemy.CompareTag(TagToDamage)) {
            enemy.AdjustHealth(-Damage);
          }
        }

        FireDoorCheck(hit.gameObject.GetComponentInParent<IFireDoor>());
        FrostDoorCheck(hit.gameObject.GetComponentInParent<IFrostDoor>());

        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles){
          GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
          curTrail.transform.parent = null;
          Destroy(curTrail, 3f);
        }
        Destroy(projectileParticle, 3f);
        Destroy(impactParticle, 5f);
        Destroy(gameObject);
        //projectileParticle.Stop();
        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++) {
          ParticleSystem trail = trails[i];
          if (!trail.gameObject.name.Contains("Trail"))
            continue;

          trail.transform.SetParent(null);
          Destroy(trail.gameObject, 2);
        }
      }
    }

    void OnTriggerEnter(Collider other) {
      if(other.isTrigger){ // should do nothing if we hit a trigger
        return;
      }

      if (!hasCollided && other.tag != IgnoreTag) {
        hasCollided = true;
        //transform.DetachChildren();
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        var audsrc = impactParticle.GetComponent<AudioSource>();
        if(audsrc != null && LowerVolume){
          audsrc.volume = Volume;
        }
        if (other.gameObject.CompareTag("Destructible")){// Projectile will destroy objects tagged as Destructible
          Destroy(other.gameObject);
        }

        var enemies = other.gameObject.GetComponents<Damagable>();
        foreach(var enemy in enemies) {
          if(enemy != null && enemy.CompareTag(TagToDamage)) {
            enemy.AdjustHealth(-Damage);
          }
        }

        FireDoorCheck(other.gameObject.GetComponentInParent<IFireDoor>());
        FrostDoorCheck(other.gameObject.GetComponentInParent<IFrostDoor>());

        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles){
          GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
          curTrail.transform.parent = null;
          Destroy(curTrail, 3f);
        }
        Destroy(projectileParticle, 3f);
        Destroy(impactParticle, 5f);
        Destroy(gameObject);
        //projectileParticle.Stop();
        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++) {
          ParticleSystem trail = trails[i];
          if (!trail.gameObject.name.Contains("Trail"))
            continue;

          trail.transform.SetParent(null);
          Destroy(trail.gameObject, 2);
        }
      }
    }

    void FireDoorCheck(IFireDoor door){
      if(door == null) return;
      if(Type == ElementalType.Fire){
        door.Open();
      }
    }

    void FrostDoorCheck(IFrostDoor door){
      if(door == null) return;
      if(Type == ElementalType.Frost){
        door.Open();
      }
    }
  }
}
