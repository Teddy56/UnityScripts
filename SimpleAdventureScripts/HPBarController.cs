using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {

	Slider slider;
	float hp = 0;

	void Start () {
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
	}

	void Update () {
		hp += 1f;
		if (hp > slider.maxValue) {
			hp = slider.minValue;
		}

		slider.value = hp;
	}
}
