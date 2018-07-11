using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private Animator am;
  private Rigidbody2D rb;

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
    playerSpeed = 3f;
    am = gameObject.GetComponent<Animator>();
    rb = gameObject.GetComponent<Rigidbody2D>();
  }

  void SetPlayerDirection(PlayerDirection dir){
    if (PlayerDirection.IsDefined(typeof(PlayerDirection), dir)) {
      Debug.LogError("Given an invalid player direction: " + dir);
    }

    playerDir = dir;
  }

  void MovePlayer() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");
    Vector3 move = new Vector3(horizontal, vertical, 0f);
    Vector3 position = transform.position;

    if (move.y > 0) {
      position += Vector3.up;
    }
    if (move.y < 0) {
      position += Vector3.down;
    }
    if (move.x > 0) {
      position += Vector3.right;
    }
    if (move.x < 0) {
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