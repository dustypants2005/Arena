using UnityEngine;

public enum ElementalType {
  None,
  Any,
  Fire,
  Frost,
  Spark

}

public class BulletCollision : MonoBehaviour {
  public float Damage = 1f;
  public bool SetInvulnerable = false;
  public float InvulnerableDuration = 1f;
  public ElementalType Type = ElementalType.None;
  private void OnCollisionEnter(Collision other) {
    var hp = other.transform.GetComponent<Damageable>();
    if (!hp.NullCheck()) {
      hp.AdjustHealth(-Damage, SetInvulnerable, InvulnerableDuration);
    }
    var bullet = transform.parent.GetComponent<UbhBullet>();
    if (!bullet.NullCheck()) {
      UbhObjectPool.instance.ReleaseBullet(transform.parent.GetComponent<UbhBullet>());
    }
  }
}