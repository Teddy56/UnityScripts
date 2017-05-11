using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour {
	
	private GameObject player = null;
	private Vector3 offset = Vector3.zero;
	public  float speed = 0.5f;
	private Vector3 distance = Vector3.zero ;
	Vector3 newPosition;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}

	void Update(){
		//Debug.Log ("");
	}

	void LateUpdate () {
		newPosition = transform.position;
		newPosition.x = player.transform.position.x + offset.x;
		newPosition.y = player.transform.position.y + offset.y;
		newPosition.z = player.transform.position.z + offset.z;
		/*
		if(Input.GetButtonDown("CameraReset")){
			//newPosition.y = player.transform.position.y + offset.y;
			newPosition.z = player.transform.position.z + offset.z;
		}

		if (Input.GetAxis ("Horizontal2") != 0 || Input.GetAxis ("Vertical2") != 0) {
			//distance = new Vector3 (0, Input.GetAxis ("Vertical2") * speed, -Input.GetAxis ("Horizontal2") * speed);
			distance = new Vector3 (0, 0, -Input.GetAxis ("Horizontal2") * speed);
		} else {
			distance = Vector3.zero;
		}
		*/
		distance = Vector3.zero;
			
		transform.position = newPosition + distance;
	}

	/*
	GameObject targetObj;
	Vector3 targetPos;

	void Start () {
		targetObj = GameObject.FindGameObjectWithTag ("Player");;
		targetPos = targetObj.transform.position;
	}

	void Update() {
		// targetの移動量分、自分（カメラ）も移動する
		transform.position += targetObj.transform.position - targetPos;
		targetPos = targetObj.transform.position;

		// マウスの右クリックを押している間
		if (Input.GetMouseButton(1)) {
			// マウスの移動量
			float mouseInputX = Input.GetAxis("Mouse X");
			// targetの位置のY軸を中心に、回転（公転）する
			transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);
		}
	}   */
}
