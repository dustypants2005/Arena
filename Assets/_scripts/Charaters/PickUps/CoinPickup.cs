using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dustypants.Managers;

namespace dustypants.Characters.Pickups {
  [RequireComponent(typeof(Collider))]
  public class CoinPickup : MonoBehaviour {
    public int Amount = 1;
    public float RotateSpeed = 30f;
    // TODO: sound effect, particles, ect for on pickup.

    void Awake() {
      transform.rotation = Quaternion.Euler(0, transform.position.x % 360 + transform.position.z % 360, 0);
    }

    void Update() {
      transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        CoinManager.instance.Add(Amount);
        Destroy(gameObject);
      }
    }
  }
}
