using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************************************************
*・プレイヤーコントローラ
*  CharactorContorollerコンポーネント と Rigitbodyコンポーネントをアタッチしたオブジェクトにアタッチすることで
*  任意のキーで移動、ジャンプさせることができます.
*  重力やジャンプ力、移動速度はInspecterタブから変更できます.
* 
*・操作方法 
*  移動     : w,a,s,d または ↑,←,↓,→ 
*  ダッシュ : 左shiftキーを押しながら移動
*  ジャンプ : spaceキー
**********************************************************************/

public class PlayerController : MonoBehaviour {

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
	//public  Slider hpBar;
	// HP
	private float hp;
	// ダメージを受けているか確認するフラグ
	private bool isDamage = false;

	// ゲーム開始時の初期位置
	Vector3 startPos = Vector3.zero;
	// チェックポイントの位置
	Vector3 checkPoint;

	void Start () {
		// ゲーム開始時の初期位置を保存
		startPos = transform.position;
		// キャラクターコントローラを設定
		controller = GetComponent<CharacterController>();
		// HPバーの最大値からHPを設定
		//hp = hpBar.maxValue;
		// チェックポイントの位置を初期化
		checkPoint = transform.position;
	}

	void Update () {
		charactorMove ();
		charactorTurn ();
		if(hp <= 0){
			//StartCoroutine("returnCheckPoint");
		}
	}

	// プレイヤーキャラクターの移動を制御
	void charactorMove(){
		if (controller.isGrounded) { // 地面についているか判定
			// 重力の初期化
			moveDirection.y = 0;
			// Spaceキーでジャンプ
			if (Input.GetKey(KeyCode.Space)) {
				moveDirection.y = jumpPower; // ジャンプするベクトルの代入
			}

			// 左shiftキーを押しながら移動でダッシュ、押していないときは通常速度で移動
			if (Input.GetKey(KeyCode.LeftShift)) {
				speed = runSpeed;
			} else {
				speed = walkSpeed;
			}

		}

		// w,a,s,d または ↑,←,↓,→を押した際に移動
		moveDirection.x = Input.GetAxis ("Horizontal") * speed;
		moveDirection.z = Input.GetAxis ("Vertical") * speed;
		/*
		if (Input.GetButton ("Turn") == true) {
			moveDirection.x = 0;
			moveDirection.z = 0;
		}
*/
		// y軸方向に重力をかける
		moveDirection.y -= gravity * Time.deltaTime; 
		controller.Move(moveDirection * Time.deltaTime); //キャラクターを動かす処理
	}

	// プレイヤーキャラクターの方向転換時にキャラクターを回転
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

	// Colliderを持つ他のオブジェクトに触れている間呼ばれる
	void OnTriggerStay(Collider hit) {
		/*
		if(!isDamage){
			if (hit.gameObject.CompareTag("Enemy") && hp > 0) {
				hp -= 10f;
				hpBar.value = hp;
				StartCoroutine("Damage", 0.5f);
			}

		}*/

	}

	// Colliderを持つ他のオブジェクトに触れた時に呼ばれる
	void OnTriggerEnter(Collider hit) {
		/*
		if (hit.gameObject.CompareTag("FallArea")) {
			hp = 0;
		}

		if(hit.gameObject.CompareTag("CheckPoint")){
			checkPoint = hit.gameObject.transform.position;
		}
		*/

	}

	private IEnumerator Damage(float time) {
		isDamage = true;
		yield return new WaitForSeconds(time);
		isDamage = false;
	}
	/*
	private IEnumerator returnCheckPoint() {
		yield return new WaitForSeconds(0.1f);
		hp = hpBar.maxValue;
		hpBar.value = hp;
		transform.position = checkPoint;
	}
	*/

}
