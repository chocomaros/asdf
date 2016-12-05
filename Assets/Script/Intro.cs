using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	public GameObject Movie;
	public MovieTexture movieTexutre;
	private AudioSource audio;

	private enum State{MOVIE_PLAYING, SELECT_UI};
	private State state = State.MOVIE_PLAYING;

	public Button btStart, btExit;

	// Use this for initialization
	void Start () {
		btStart.onClick.AddListener (btStartClick);
		btExit.onClick.AddListener (btExitClick);
		btStart.enabled = false;
		btExit.enabled = false;
		movieTexutre = Movie.GetComponent<RawImage> ().texture as MovieTexture;
		//Movie.GetComponent<RawImage> ().texture = movieTexutre as MovieTexture;
		//audio = GetComponent<AudioSource> ();
		//audio.clip = movieTexutre.audioClip;
		movieTexutre.Play ();
		//audio.Play ();
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
				SelectUI ();
				break;
			}
			yield return null;
		}
	}

	void MoviePlaying(){
		if (Input.GetMouseButtonDown (0)) {
			movieTexutre.Stop ();
			//audio.Stop ();
			DestroyImmediate(Movie);
			state = State.SELECT_UI;
		}
	}

	void SelectUI(){
		btStart.enabled = true;
		btExit.enabled = true;
	}

	private void btStartClick(){
		Application.LoadLevel (1);
	}

	private void btExitClick(){
		Application.Quit ();
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
