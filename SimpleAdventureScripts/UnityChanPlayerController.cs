using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UnityChanPlayerController : MonoBehaviour {

	private CharacterController controller;
	private Vector3 moveDirection;

	public  float gravity = 10f;
	public  float jumpPower = 10f;
	private float speed;
	public  float walkSpeed = 5f;
	public  float runSpeed = 8f;
	public float rotationSpeed = 360f;

	Animator animator;

	void Start () {
		controller = GetComponent<CharacterController> ();
		animator = GetComponentInChildren<Animator> ();
	}

	void Update () {
		charactorTurn ();
		charactorMove ();
	}

	// 移動操作
	void charactorMove(){
		if (controller.isGrounded) { //地面についているか判定
			moveDirection.y = 0;
			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpPower; //ジャンプするベクトルの代入
				animator.SetTrigger ("JumpTrigger");
			}
		}

		// 移動速度を変更
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.LeftShift)) {
			speed = runSpeed;
		} else {
			speed = walkSpeed;
		}

		moveDirection.x = Input.GetAxis ("Horizontal") * speed; // 左右移動

		moveDirection.y -= gravity * Time.deltaTime; // 重力計算
		controller.Move(moveDirection * Time.deltaTime); // 動かす処理

		// アニメーション操作
		animator.SetFloat ("Speed", controller.velocity.magnitude);
	}

	// 操作キャラクターの振り向き制御
	void charactorTurn(){
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		if (direction.sqrMagnitude > 0.01f) {
			Vector3 forward = Vector3.Slerp (
				transform.forward, 
				direction,
				rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
			);
			transform.LookAt (transform.position + forward);
		}
	}
		
}
