using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObj : MonoBehaviour {

	void OnTriggerStay(Collider hit) {
		if (hit.gameObject.CompareTag("Player")) {
			Debug.Log ("Goal");
		}
	}

}
