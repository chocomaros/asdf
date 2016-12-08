using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBackground : MonoBehaviour {

	public AudioSource Audio;
	public AudioClip NormalRoomMusic;
	public AudioClip BossRoomMusic;

	// Use this for initialization
	void Start () {
		Audio.clip = NormalRoomMusic;
		Audio.loop = true;
		Audio.volume = 0.3f;
		Audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBackgroundMusic(Room.RoomType roomType){
		if (roomType == Room.RoomType.BOSS) {
			Audio.Stop ();
			Audio.clip = BossRoomMusic;
			Audio.Play();
		} else {
			if (!Audio.clip.Equals (NormalRoomMusic)) {
				Audio.Stop ();
				Audio.clip = NormalRoomMusic;
				Audio.Play();
			}
		}
	}
}
