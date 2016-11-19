using UnityEngine;
using System.Collections;

public class ArrowDisappear : MonoBehaviour {

	public GameObject arrow;


	public void OnActive(){
		arrow.SetActive (true);
	}

	public void OffActive(){
		arrow.SetActive (false);

	}
}
