using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

	private GameObject checkPoint;
	private GameObject player;
/*
	void Start() {
		checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
*/
	public void ReturnCharacter() {
		checkPoint = GameObject.FindGameObjectWithTag("CheckPoint");
		player = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine("returnCharacter", player);
	}

	private IEnumerator returnCharacter(GameObject character) {
		yield return new WaitForSeconds(1f);

		character.transform.position = checkPoint.transform.position;
	}
}
