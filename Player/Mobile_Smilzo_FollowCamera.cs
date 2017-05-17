using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Mobile_Smilzo_FollowCamera : MonoBehaviour {

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

		float mouseInputX = CrossPlatformInputManager.GetAxisRaw ("Horizontal2");
		transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);

	}   
}
