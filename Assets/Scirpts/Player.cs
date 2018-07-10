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
    Vector3 move;
  }

  // Use this for initialization
  void Start () {
  }
  // Update is called once per frame
  void Update () {
    MovePlayer();
  }

}
