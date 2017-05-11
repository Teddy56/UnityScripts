using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDecision : MonoBehaviour {

	private GameObject checkPoint;

	void Start() {
		checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
	}

	private void OnTriggerEnter(Collider hit) {
		if (hit.CompareTag("Player")) {
			StartCoroutine("returnCharacter", hit.gameObject);
		}
	}

	private IEnumerator returnCharacter(GameObject character) {
		yield return new WaitForSeconds(1f);

		character.transform.position = checkPoint.transform.position;
	}
}
