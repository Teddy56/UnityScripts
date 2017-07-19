using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * スタート地点のオブジェクトにアタッチして使用
 * (colliderのisTriggerにチェックを入れてください)
 */

public class StartObj : MonoBehaviour {

	void OnTriggerStay(Collider hit) {
		if (hit.gameObject.CompareTag("Player")) {
			Debug.Log ("Start");
		}
	}

}
