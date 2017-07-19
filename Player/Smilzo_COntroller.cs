using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Smilzo_COntroller : MonoBehaviour {

	// キャラクターコントローラ
	private CharacterController controller;
	// 進行方向
	private Vector3 moveDirection;

	// 重力
	public  float gravity = 10f;
	// ジャンプ力
	public  float jumpPower = 10f;
	// 移動スピード
	private float speed;
	// 通常移動の速さ
	public  float walkSpeed = 5f;
	// 走っている時の速さ
	public  float runSpeed = 8f;
	// 方向転換をする速さ
	public  float rotationSpeed = 360f;

	// HPバー
	public  Slider hpBar;
	// HP
	private float hp;
	// ダメージを受けているか確認するフラグ
	private bool isDamage = false;

	// ゲーム開始時の初期位置
	Vector3 startPos = Vector3.zero;
	// チェックポイントの位置
	Vector3 checkPoint;

	// アニメーター
	Animator animator;
	private  int speedId;

	void Start () {
		// ゲーム開始時の初期位置を保存
		startPos = transform.position;
		// キャラクターコントローラを設定
		controller = GetComponent<CharacterController>();
		// HPバーの最大値からHPを設定
		//hp = hpBar.maxValue;
		// チェックポイントの位置を初期化
		checkPoint = transform.position;
		// Animatorを設定
		animator = GetComponent<Animator>();
		// 
		speedId = Animator.StringToHash("Speed");
	}

	void Update () {
		charactorMove ();
		charactorTurn ();
		if(hp <= 0){
			StartCoroutine("returnCheckPoint");
		}
	}

	// プレイヤーキャラクターの移動を制御
	void charactorMove(){
		if (controller.isGrounded) { // 地面についているか判定
			// 重力の初期化
			moveDirection.y = 0;
			// Spaceキーでジャンプ
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpPower; // ジャンプするベクトルの代入
			}

			// 左shiftキーを押しながら移動でダッシュ、押していないときは通常速度で移動
			if (Input.GetButton ("Dash")) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}

			// 
			animator.SetFloat (speedId, speed);

			//
			if (Input.GetButtonDown ("Fire1")) {
				animator.SetBool ("Attack", true);
			} else {
				animator.SetBool ("Attack", false);
			}

		}

		Vector3 cameraForward = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)).normalized;
		Debug.Log (cameraForward);

		// w,a,s,d または ↑,←,↓,→を押した際に移動
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			//moveDirection.x = Input.GetAxis ("Horizontal") * speed;
			//moveDirection.z = Input.GetAxis ("Vertical") * speed;
			Vector3 moveDirect = cameraForward * Input.GetAxis ("Vertical") * speed + Camera.main.transform.right * Input.GetAxis ("Horizontal") * speed;
			moveDirection.x = moveDirect.x;
			moveDirection.z = moveDirect.z;
			animator.SetBool ("Move", true);
		} else {
			moveDirection.x = 0;
			moveDirection.z = 0;
			animator.SetBool ("Move", false);

		}
			
		if (Input.GetButton ("Turn")) {
			moveDirection.x = 0;
			moveDirection.z = 0;
		}

		// y軸方向に重力をかける
		moveDirection.y -= gravity * Time.deltaTime; 
		//キャラクターを動かす処理
		controller.Move(moveDirection * Time.deltaTime); 
	}

	// プレイヤーキャラクターの方向転換時にキャラクターを回転
	void charactorTurn(){
		//Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		Vector3 direction = Vector3.Scale (Camera.main.transform.forward, new Vector3 (1, 0, 1)).normalized * Input.GetAxis ("Vertical") + Camera.main.transform.right * Input.GetAxis ("Horizontal");

		if (direction.sqrMagnitude > 0.01f) {
			Vector3 forward = Vector3.Slerp (
				transform.forward, 
				direction,
				rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction)
			);
			transform.LookAt (transform.position + forward);
		}
	}

	// Colliderを持つ他のオブジェクトに触れている間呼ばれる
	void OnTriggerStay(Collider hit) {

		if(!isDamage){
			if (hit.gameObject.CompareTag("Enemy") && hp > 0) {
				hp -= 10f;
				hpBar.value = hp;
				StartCoroutine("Damage", 0.5f);
			}

		}

	}

	// Colliderを持つ他のオブジェクトに触れた時に呼ばれる
	void OnTriggerEnter(Collider hit) {
		
		if (hit.gameObject.CompareTag("FallArea")) {
			hp = 0;
		}

		if(hit.gameObject.CompareTag("CheckPoint")){
			checkPoint = hit.gameObject.transform.position;
		}

	}

	private IEnumerator Damage(float time) {
		isDamage = true;
		yield return new WaitForSeconds(time);
		isDamage = false;
	}

	private IEnumerator returnCheckPoint() {
		yield return new WaitForSeconds(0.1f);
		hp = hpBar.maxValue;
		hpBar.value = hp;
		transform.position = checkPoint;
	}

}
