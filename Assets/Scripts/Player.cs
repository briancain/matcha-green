using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private Animator am;
  private Rigidbody2D rb;
  private bool isIdle;
  private bool playerCollide;

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
    playerCollide = false;
    am = gameObject.GetComponent<Animator>();
    rb = gameObject.GetComponent<Rigidbody2D>();
  }


  /*
    Redo animation controller
    Instead do...

              Up
              ^
             |||
              v
    Left <=> Idle <=> Right
              ^
             |||
              v
             Down
    With entry pointed to idle. Have two floats for horizontal and vertical
    direction and edit transitions this way. Double arrows for every state.
   */
  void SetPlayerDirection(PlayerDirection dir, Vector3 moveDir){
    playerDir = dir;

    switch(playerDir) {
      case PlayerDirection.NORTH:
        am.SetBool("Backward", true);
        am.SetBool("Forward", false);
        am.SetBool("Right", false);
        am.SetBool("Left", false);
        break;
      case PlayerDirection.SOUTH:
        am.SetBool("Forward", true);
        am.SetBool("Backward", false);
        am.SetBool("Right", false);
        am.SetBool("Left", false);
        break;
      case PlayerDirection.EAST:
        am.SetBool("Right", true);
        am.SetBool("Backward", false);
        am.SetBool("Forward", false);
        am.SetBool("Left", false);
        break;
      case PlayerDirection.WEST:
        am.SetBool("Left", true);
        am.SetBool("Backward", false);
        am.SetBool("Right", false);
        am.SetBool("Forward", false);
        break;
    }
  }

  void MovePlayer() {
    float horizontal = 0f;
    float vertical = 0f;

    // Ensure that player moves in a single direction
    if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0) {
      if (Input.GetAxisRaw("Vertical") !=  0) {
        horizontal = Input.GetAxisRaw("Horizontal");
      } else if (Input.GetAxisRaw("Horizontal") !=  0) {
        vertical = Input.GetAxisRaw("Vertical");
      }
    } else {
      horizontal = Input.GetAxisRaw("Horizontal");
      vertical = Input.GetAxisRaw("Vertical");
    }

    Vector3 move = new Vector3(horizontal, vertical, 0f);
    Vector3 position = transform.position;

    if (move.y == 0f && move.x == 0f) {
      isIdle = true;
    } else {
      isIdle = false;
    }
    am.SetBool("Idle", isIdle);


    if (move.y > 0) {
      SetPlayerDirection(PlayerDirection.NORTH, move);
      position += Vector3.up;
    }
    if (move.y < 0) {
      SetPlayerDirection(PlayerDirection.SOUTH, move);
      position += Vector3.down;
    }
    if (move.x > 0) {
      SetPlayerDirection(PlayerDirection.EAST, move);
      position += Vector3.right;
    }
    if (move.x < 0) {
      SetPlayerDirection(PlayerDirection.WEST, move);
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

  void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Background") {
      playerCollide = true;
    }
  }

  void OnCollisionExit2D(Collision2D col) {
    if (col.gameObject.tag == "Background") {
      playerCollide = false;
    }
  }

}
