using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Mobile_Gun_Shot : MonoBehaviour {

	public GameObject bullet;
	public Transform muzzle;
	public float bulletPower;
	public float bulletDestroyTime = 5;
	//public AudioClip gunSoundAudioClip;
	//AudioSource gunSoundAudioSource;

	void Start () {
		//gunSoundAudioSource = gameObject.GetComponent<AudioSource> ();
	}

	void Update () {
		if(CrossPlatformInputManager.GetButtonDown("Fire1")){
			//gunSoundAudioSource.PlayOneShot (gunSoundAudioClip);
			Shot ();
		}

	}

	void Shot(){
		GameObject bulletInstance = GameObject.Instantiate (bullet, muzzle.position, muzzle.rotation);
		bulletInstance.GetComponent<Rigidbody> ().AddForce (bulletInstance.transform.forward*bulletPower);
		Destroy (bulletInstance, bulletDestroyTime);
	}
}
