using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowGun : MonoBehaviour {

	private GameObject player = null;
	private Vector3 offset = Vector3.zero;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		Vector3 newPosition = transform.position;
		Quaternion newRotation = transform.rotation;
		newPosition.x = player.transform.position.x + offset.x;
		newPosition.y = player.transform.position.y + offset.y;
		newPosition.z = player.transform.position.z + offset.z;

		newRotation.x = player.transform.rotation.x;
		newRotation.y = player.transform.rotation.y;
		newRotation.z = player.transform.rotation.z;

		transform.position = newPosition;
	}
}
