﻿using UnityEngine;
using System.Collections;

public class ArrowShoot : MonoBehaviour {

	public GameObject arrow;
	public ArrowDisappear arrowGUI;
	public Transform arrowPosition;
	public Transform tCamera;

	private const float MAX_POWER = 0.5f;
	private const float POWER_INCREASE_UNIT = 0.005f;
	private const float APPEAR_TIME = 1.0f; // 1초

	private float power;
	private bool isAppear = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isAppear) {
			if(Input.GetMouseButtonDown(0)){
				power = POWER_INCREASE_UNIT;
			}
			if(Input.GetMouseButton(0)){
				if(power <= MAX_POWER - POWER_INCREASE_UNIT){
					power += POWER_INCREASE_UNIT;
				} 
			}
			if(Input.GetMouseButtonUp(0)){
				arrow.GetComponent<ArrowReady>().power = power;
				arrow.GetComponent<ArrowReady>().angle = 360f - arrowPosition.eulerAngles.x;
				Instantiate(arrow,arrowPosition.position,arrowPosition.rotation);
				arrowGUI.OffActive();
				isAppear = false;
				Invoke("arrowGUIOn",APPEAR_TIME);
			}
		} else {

		}
	}

	private void arrowGUIOn(){
		arrowGUI.OnActive ();
		isAppear = true;
	}
}
