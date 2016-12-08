using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxScript : MonoBehaviour {
	public Animator animator;
	public GameObject idleBox,destoryBox,myitem;
	bool destroyed=false;
	// Use this for initialization
	void Start () {
		destoryBox.SetActive (false);
		myitem.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void DestoryBox(){
		if (!destroyed) {
			destroyed = true;
			animator.SetTrigger ("bang");
			idleBox.SetActive (false);
			myitem.SetActive ( true);
			destoryBox.SetActive (true);
			Destroy (gameObject, 2f);

		}
	}
}
