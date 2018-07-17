using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private Animator am;
  private Rigidbody2D rb;
  private bool isIdle;

  private enum PlayerDirection {
    NORTH,
    SOUTH,
    EAST,
    WEST
  }

  private PlayerDirection playerDir;
  private float playerSpeed;


  void Awake() {
    InitVars();
  }

  void InitVars() {
    playerSpeed = 5f;
    isIdle = true;
    am = gameObject.GetComponent<Animator>();
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  void SetPlayerDirection(PlayerDirection dir){
    playerDir = dir;
  }

  void MovePlayer() {
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector3 move = new Vector3(horizontal, vertical, 0f);
    Vector3 position = transform.position;

    if (move.y > 0) {
      SetPlayerDirection(PlayerDirection.NORTH);
      position += Vector3.up;
    }
    if (move.y < 0) {
      SetPlayerDirection(PlayerDirection.SOUTH);
      position += Vector3.down;
    }
    if (move.x > 0) {
      SetPlayerDirection(PlayerDirection.EAST);
      position += Vector3.right;
    }
    if (move.x < 0) {
      SetPlayerDirection(PlayerDirection.WEST);
      position += Vector3.left;
    }

    transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime*playerSpeed);
  }

  // Use this for initialization
  void Start () {
  }
  // Update is called once per frame
  void Update () {
    MovePlayer();
  }

}
