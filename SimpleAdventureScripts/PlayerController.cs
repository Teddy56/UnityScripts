using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private CharacterController controller;
	private Vector3 moveDirection;

	public  float gravity = 10f;
	public  float jumpPower = 10f;
	private float speed;
	public  float walkSpeed = 5f;
	public  float runSpeed = 8f;
	public  float rotationSpeed = 360f;

	public  Slider hpBar;
	private float hp;
	private bool isDamage = false;

	Vector3 startPos = Vector3.zero;
	Vector3 returnPoint;
	Vector3 cameraForward = Vector3.zero;

	void Start () {
		startPos = transform.position;
		controller = GetComponent<CharacterController>();
		hp = hpBar.maxValue;
		returnPoint = transform.position;
	}

	void Update () {
		
		charactorMove ();
		charactorTurn ();

		if(hp <= 0){
			StartCoroutine("returnCharacter");
		}
		//Debug.Log (returnPoint.transform.position);

	}

	void charactorMove(){
		if (controller.isGrounded) { //地面についているか判定
			moveDirection.y = 0;
			if (Input.GetButtonDown("Jump")) {
				moveDirection.y = jumpPower; //ジャンプするベクトルの代入
			}
			if (Input.GetButton("Dash")) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}
		}

		cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		moveDirection.x = Input.GetAxis ("Horizontal") * speed;
		moveDirection.z = Input.GetAxis ("Vertical") * speed;

		if (Input.GetButton ("Turn") == true) {
			moveDirection.x = 0;
			moveDirection.z = 0;
		}

		moveDirection.y -= gravity * Time.deltaTime; //重力計算
		controller.Move(moveDirection * Time.deltaTime); //cubeを動かす処理
	}

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

	void OnTriggerStay(Collider hit) {

		if(!isDamage){
			if (hit.gameObject.CompareTag("Enemy") && hp > 0) {
				hp -= 10f;
				hpBar.value = hp;
				StartCoroutine("Damage", 0.5f);
			}

		}

	}

	void OnTriggerEnter(Collider hit) {
		if (hit.gameObject.CompareTag("FallArea")) {
			hp = 0;
		}

		if(hit.gameObject.CompareTag("CheckPoint")){
			returnPoint = hit.gameObject.transform.position;
		}

	}

	private IEnumerator Damage(float time) {
		isDamage = true;
		yield return new WaitForSeconds(time);
		isDamage = false;
	}

	private IEnumerator returnCharacter() {
		yield return new WaitForSeconds(0.1f);
		hp = hpBar.maxValue;
		hpBar.value = hp;
		transform.position = returnPoint;
	}

}
