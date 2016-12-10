using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public GameObject ButtonMain;

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		ButtonMain.GetComponent<Button> ().onClick.AddListener (ButtonMainClick);
	}
	private void ButtonMainClick(){
		SceneManager.LoadScene (0);
	}
}
