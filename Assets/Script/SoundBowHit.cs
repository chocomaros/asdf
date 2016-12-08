using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBowHit : MonoBehaviour {

	public AudioClip Hit;
	public AudioClip HeadShot;
	public AudioSource Audio;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "enemy") {
			Audio.PlayOneShot(Hit);
		} else if (collider.tag == "enemy_head") {
			Audio.PlayOneShot (HeadShot);
		}
	}
}
