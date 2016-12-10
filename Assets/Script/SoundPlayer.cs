using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	public enum SpeakType{Heal,Teleport,Hurt,PowerUp};

	public AudioClip Heal;
	public AudioClip Teleport;
	public AudioClip Hurt;
	public AudioClip PowerUp;

	public AudioSource Audio;

	public void Speak(SpeakType speakType){
		if (speakType == SpeakType.Heal) {
			Audio.PlayOneShot (Heal);
		} else if (speakType == SpeakType.Teleport) {
			Audio.PlayOneShot (Teleport);
		} else if (speakType == SpeakType.Hurt) {
			Audio.PlayOneShot (Hurt);
		} else if (speakType == SpeakType.PowerUp) {
			Audio.PlayOneShot (PowerUp);
		}
	}
}
