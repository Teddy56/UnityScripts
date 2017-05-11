using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDecision : MonoBehaviour {

	private GameObject returnPoint;

	void Start() {
		returnPoint = GameObject.FindGameObjectWithTag("CheckPoint");
	}

	private void OnTriggerEnter(Collider hit) {
		if (hit.CompareTag("Player")) {
			StartCoroutine("returnCharacter", hit.gameObject);
		}
	}

	private IEnumerator returnCharacter(GameObject character) {
		yield return new WaitForSeconds(1f);

		character.transform.position = returnPoint.transform.position;
	}
}
