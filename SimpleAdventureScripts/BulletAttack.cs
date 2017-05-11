using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour {

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Enemy") {
			Destroy (col.gameObject);
		}
		Destroy (gameObject);
	}

}
