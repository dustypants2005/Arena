using UnityEngine;
using dustypants.Managers;
using UnityEngine.SceneManagement;

namespace dustypants.Characters.Pickups
{
  [RequireComponent(typeof(Collider))]
  public class CoinPickup : MonoBehaviour {
    public int Amount = 1;
    public float RotateSpeed = 30f;
    public float ID;

    // TODO: sound effect, particles, ect for on pickup.

    void Awake() {
      transform.rotation = Quaternion.Euler(0, transform.position.x % 360 + transform.position.z % 360, 0);
      if(ID == 0)
        ID = transform.position.sqrMagnitude;
    }

    void Update() {
      transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other) {
      if(other.CompareTag("Player")){
        var cm = CoinManager.instance;
        cm.Add(Amount);
        cm.AddOrUpdate(SceneManager.GetActiveScene().name, ID, false);
        this.gameObject.SetActive(false);
      }
    }
  }
}
