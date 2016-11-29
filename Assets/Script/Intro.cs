using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource audio;

	private enum State{MOVIE_PLAYING, SELECT_UI};
	private State state = State.MOVIE_PLAYING;

	public Button btStart, btExit;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();
		StartCoroutine ("IntroState");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator IntroState(){
		while (true) {
			switch (state) {
			case State.MOVIE_PLAYING: 
				MoviePlaying ();
				break;
			case State.SELECT_UI:

				break;
			}
			yield return null;
		}
	}

	void MoviePlaying(){
		if (Input.GetMouseButtonDown (0)) {
			movie.Stop ();
			audio.Stop ();
			DestroyImmediate(movie,false);
			state = State.SELECT_UI;
			//Application.LoadLevel (1);
		}
	}

	void SelectUI(){

	}
}
