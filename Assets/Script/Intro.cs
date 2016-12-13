using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	public GameObject Movie;
	private MovieTexture movieTexture;

	private enum State{MOVIE_PLAYING, SELECT_UI};
	static State state = State.MOVIE_PLAYING;

	public Button btStart, btExit;

	// Use this for initialization
	void Start () {
		btStart.onClick.AddListener (btStartClick);
		btExit.onClick.AddListener (btExitClick);
		btStart.enabled = false;
		btExit.enabled = false;
		movieTexture = Movie.GetComponent<RawImage> ().texture as MovieTexture;

		StartCoroutine ("IntroState");
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
		movieTexture.Play ();
		if (Input.GetMouseButtonDown (0)) {
			movieTexture.Stop ();
			state = State.SELECT_UI;
		}
	}

	void SelectUI(){
		Movie.SetActive(false);
		btStart.enabled = true;
		btExit.enabled = true;
	}

	private void btStartClick(){
		btStart.GetComponentInChildren<Text> ().color = Color.yellow;
		SceneManager.LoadScene (1);
	}

	private void btExitClick(){
		btExit.GetComponentInChildren<Text> ().color = Color.yellow;
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
