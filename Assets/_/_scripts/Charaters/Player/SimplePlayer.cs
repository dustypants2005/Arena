using dustypants.Environment;
using dustypants.Managers;
using dustypants.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace dustypants.Characters.Player {
  [DisallowMultipleComponent]
  [RequireComponent(typeof(Health))]
  [RequireComponent(typeof(CharacterController))]
  public class SimplePlayer : MonoBehaviour {
    protected static SimplePlayer s_Instance;
    public static SimplePlayer instance { get { return s_Instance; } }
    public Weapon CurrentWeapon;
    public Transform WeaponMount;

    public PlayerInfo Info;
    [HideInInspector]
    public bool isDisabled = false;

    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI CoinsCollectedText;
    [SerializeField] private float speed = 12F;
    [SerializeField] private float gravity = 30F;
    [SerializeField] private float xNormalLookSpeed = 5;
    [SerializeField] private float yNormalLookSpeed = 5;
    [SerializeField] private float SlowLookSpeed = 2;


    [Header("Dash")]
    [SerializeField] private float dash = 50f;
    [SerializeField] private float dashCooldown = 3f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private Vector3 dashDirection = Vector3.zero;
    private float nextTimeToDash { get; set; }
    private bool isDashing = false;

    [Header("Jump")]
    [SerializeField] private GameObject jumpCloud;
    [SerializeField] private float jumpSpeed = 10F;
    [SerializeField] private bool canAirJump = true; // can we air jump
    private bool isAirJump = true; // is air jump ready
    private bool isWallJumping = false; // is air jump ready


    [Header("WallJump")]
    [SerializeField] private GameObject wallCloud;
    [SerializeField] private float wallCloudLifeTime = 2f;

    [Header("camera")]
    [SerializeField] private Transform camMount;
    private Camera cam;
    [SerializeField] private float fov = 90;
    [SerializeField] private float zoomedFov = 30;
    [SerializeField] private List<GameObject> blockingView;

    [Header("Other")]
    public SimpleMouseLook mouseLook;
    public TriggerEvent e;
    [SerializeField] private Transform weaponMount;
    [SerializeField] private Transform lookAtTarget;


    public Vector3 moveDirection = Vector3.zero;
    public Vector3 lastMoveDirection = Vector3.zero;
    private Vector3 wallJumpDirection = Vector3.zero;
    [Header("Knockback")]
    public Vector3 knockback = Vector3.zero;
    public bool isKnockback = false;
    public float knockbackDuration = 1f;
    public float knockbackTime = 1f;
    public float AddedverticalVelocity = 0f;

    private float verticalVelocity = 0f;
    private bool isJumping = false;
    private int layermask;
    private CharacterController controller;
    private bool isZoomed = false;
    private bool hasFired = false;

    private Animator anim;

    [SerializeField] private Vector2 moveInput = Vector2.zero;

    private WeaponsManager wm;

    void Awake() {
      s_Instance = this;
      controller = GetComponent<CharacterController>();
      cam = camMount.GetComponentInChildren<Camera>();
      anim = GetComponentInChildren<Animator>();
      wm = WeaponsManager.instance;
      mouseLook.Init(transform, camMount.transform);
      layermask = 1 << 8;
      layermask = ~layermask;
      ResetDash();
    }

    void Start() {
      UpdateInfo();
      cam.fieldOfView = fov;
      wm.InitWeapons();
      CurrentWeapon = wm.GetCurrentWeapon().GetComponent<Weapon>();
      SetWeaponController();
    }

    void Update() {
      if (isDisabled) return;

      anim.SetFloat("Horizontal", Input.GetAxis("L_XAxis"));
      anim.SetFloat("Vertical", Input.GetAxis("L_YAxis"));

      if (Input.GetButtonUp("RB")) {
        CurrentWeapon = wm.NextWeapon().GetComponent<Weapon>();
        CurrentWeapon.ResetFireTime();
      }

      if (Input.GetButtonUp("LB")) {
        CurrentWeapon = wm.PreviousWeapon().GetComponent<Weapon>();
        CurrentWeapon.ResetFireTime();
      }

      if (Input.GetButton("TriggersL") || Input.GetAxisRaw("TriggersL") > .1f) {
        cam.fieldOfView = zoomedFov;
        isZoomed = true;
      } else {
        cam.fieldOfView = fov;
        isZoomed = false;
      }
      if (Input.GetButton("TriggersR") || Input.GetAxisRaw("TriggersR") > .1f) {
        if (CurrentWeapon != null) {
          if (CurrentWeapon.isSingleShot) {
            if (!hasFired) {
              CurrentWeapon.Attack();
            }
          } else {
            CurrentWeapon.Attack();
          }
          hasFired = true;
        } else { Debug.LogError("ERROR: CurrentWeapon is NULL!"); }
      } else {
        hasFired = false;
      }

      // releasing jump or falling, use normal gravity
      if (Input.GetButtonUp("A") || controller.velocity.y <= 0) {
        isJumping = false;
        isWallJumping = false;
      }

      if (Input.GetButtonUp("X") && e != null) {
        e.Invoke();
      }

      if (!isWallJumping) {
        moveDirection -= lastMoveDirection;
        moveDirection += new Vector3(Input.GetAxis("L_XAxis"), 0, Input.GetAxis("L_YAxis"));
        //moveDirection += new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
      } else {
        moveDirection = Vector3.zero;
      }

      if (isZoomed) {
        moveDirection *= .5f;
        mouseLook.XSensitivity = SlowLookSpeed;
        mouseLook.YSensitivity = SlowLookSpeed;
      } else {
        mouseLook.XSensitivity = xNormalLookSpeed;
        mouseLook.YSensitivity = yNormalLookSpeed;
      }

      if (controller.isGrounded) {
        AddedverticalVelocity = 0;
        wallJumpDirection = Vector3.zero;
        isAirJump = true;
        verticalVelocity = -1;
        if (Input.GetButtonDown("A")) {
          verticalVelocity = jumpSpeed;
          isJumping = true;
          Instantiate(jumpCloud, transform.position, transform.rotation);
        }
      } else {
        if (!isJumping) {
          verticalVelocity -= gravity * Time.deltaTime;
        } else {// light jump while we hold the jump button
          verticalVelocity -= gravity / 2 * Time.deltaTime;
        }
        moveDirection += wallJumpDirection; // add wall jump to move direction
      }
      // Air Jump
      if (Info.CanDoubleJump && canAirJump && isAirJump && !isJumping) {
        if (Input.GetButtonDown("A")) {
          wallJumpDirection = Vector3.zero;
          verticalVelocity = jumpSpeed;
          isJumping = true;
          isAirJump = false;
          Instantiate(jumpCloud, transform.position, transform.rotation);
        }
      }
      // Dash
      if (Info.CanDash && Input.GetButtonDown("B")) {
        if (Time.time > nextTimeToDash && !isDashing) {
          nextTimeToDash = Time.time + dashCooldown;
          if (moveDirection.x != 0 || moveDirection.z != 0) {
            dashDirection = moveDirection.normalized * dash;
            isDashing = true;
            StartCoroutine(SetDashDuration());
          } else {
            dashDirection += transform.forward * dash;
            isDashing = true;
            StartCoroutine(SetDashDuration());
          }
        }
      }

      if (dashDirection != Vector3.zero) {
        moveDirection += dashDirection;
      }
      //if(knockback != null){
      //  isKnockback = true;
      //  knockbackTime = knockbackDuration + Time.deltaTime;
      //}
      // if(isKnockback){
      //   // TODO: need to find point of contact, push from point.
      //   moveDirection +=  transform.InverseTransformDirection(knockback);
      //   if(knockbackTime > Time.deltaTime ){
      //     isKnockback = false;
      //     knockback = Vector3.zero;
      //   }
      // }

      moveDirection.y = verticalVelocity;
      moveDirection.y += AddedverticalVelocity;
      mouseLook.LookRotation(transform, camMount.transform);
      controller.Move(moveDirection * Time.deltaTime);
      lastMoveDirection = moveDirection;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
      if (Info.CanWallJump && !controller.isGrounded) {
        if (hit.normal.y < .1f) { // TODO: bug; when ceiling hits head, we trigger wall jump for w/e reason, need to exclude ceililng from wall jump
          isAirJump = true;
          verticalVelocity -= gravity / 3 * Time.deltaTime;
          var cloud = Instantiate(wallCloud, hit.point, transform.rotation);
          Destroy(cloud, wallCloudLifeTime);

          if (Input.GetButtonDown("A")) {
            wallJumpDirection = hit.normal * jumpSpeed;
            verticalVelocity = jumpSpeed;
            isJumping = true;
            isWallJumping = true;
            Instantiate(jumpCloud, transform.position, transform.rotation);
          }
        }
      }
    }

    void SetWeaponController() {
      if (controller != null) {
        CurrentWeapon.SetController(controller);
        CurrentWeapon.ResetFireTime();
      }
      if (weaponMount != null) {
        //TODO: if we need to setup weapon, do it here.
      };
    }
    void ResetDash() {
      nextTimeToDash = 0;
    }

    IEnumerator SetDashDuration() {
      yield return new WaitForSeconds(dashDuration);
      isDashing = false;
      dashDirection = Vector3.zero;
    }

    public void UpdateCoins(int coins) {
      CoinText.text = coins.ToString();
      if (CoinGroupManager.instance != null && CoinsCollectedText != null) {
        var coinsCollected = CoinManager.instance.CoinSaves[SaveManager.instance.GetCurrentLevelName()];
        var max = coinsCollected.Count;
        var total = 0;
        foreach (var v in coinsCollected.Values) {
          if (!v) total++;
        }
        CoinsCollectedText.text = total + " / " + max;
      }
    }

    public void UpdateInfo() {
      var i = SaveManager.instance.data;
      if (i != null) {
        Info = i;
      } else {
        Info = new PlayerInfo();
      }
    }
  }
}
