using UnityEngine;

  /// <summary>
  /// Place on Camera Mount
  /// </summary>
  public class CameraRaycast : MonoBehaviour {
    [SerializeField] private float camDistance = 15f;
    [SerializeField] private float transparentDistance = 3f;
    [SerializeField] private float adjustmentSpeed = 1f;
    private Camera camera;
    private SimplePlayer player;
    private Vector3 defaultCameraPosition = Vector3.zero;
    private const string PlayerTag = "Player";

    void Awake() {
      player = SimplePlayer.instance;
      camera = GetComponentInChildren<Camera>();
    }

    void Start() {
      var campos = camera.gameObject.transform.localPosition;
      defaultCameraPosition = new Vector3(campos.x, campos.y, -camDistance);
    }

    void FixedUpdate() {
      var target = GetClosestCameraPosition();
      camera.gameObject.transform.localPosition = target;
    }

    Vector3 GetClosestCameraPosition() {
      var currentCamPos = GetCameraPosition(camera.transform.position);
      var camtran = camera.transform;
      camtran.localPosition = defaultCameraPosition;
      var defaultCamPos = GetCameraPosition(camtran.position);
      return currentCamPos.z >= defaultCamPos.z ? currentCamPos : defaultCamPos;
    }

    Vector3 GetCameraPosition(Vector3 camposition) {
      var direction = transform.position.Direction(camposition);
      var campos = camera.transform.localPosition;
      Debug.DrawLine(transform.position, camposition);
      var hits = Physics.RaycastAll(transform.position, direction, camDistance);
      Vector3 closestHit = ClosestHit(hits, campos);

      if (closestHit == Vector3.zero) {
        return defaultCameraPosition;
      } else {
        return closestHit;
      }
    }

    Vector3 ClosestHit(RaycastHit[] hits, Vector3 campos) {
      Vector3 closestHit = Vector3.zero;
      float closestDistance = Mathf.Infinity;
      foreach (var hit in hits) {
        if (hit.collider.isTrigger) continue;
        if (hit.collider.CompareTag(PlayerTag)) continue;
        var h = transform.position - hit.point;
        var pos = new Vector3(campos.x, campos.y, -h.magnitude);
        var d = transform.position.Distance(pos);

        if (closestDistance > d) {
          closestHit = pos;
          closestDistance = d;
        }
      }
      /* TODO: doesn't always work
       * Demo level has the bug recreated with 4 large boxes with the probuilder door created.
       */
      return closestHit;
    }
  }