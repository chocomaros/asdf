using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	public enum SpeakType{Heal,Teleport};

	public AudioClip Heal;
	public AudioClip Teleport;

	public AudioSource Audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Speak(SpeakType speakType){
		if (speakType == SpeakType.Heal) {
			Audio.PlayOneShot (Heal);
		} else if (speakType == SpeakType.Teleport) {
			Audio.PlayOneShot (Teleport);
		}
	}
}
