using UnityEngine;
using System.Collections;

public class ArrowShoot : MonoBehaviour {

	public GameObject arrow;
	public ArrowDisappear arrowGUI;
	public Transform arrowPosition;
	public Transform tCamera;
	public GameObject Aim;
	private Vector3 arrowOrigin;
	private const float MAX_POWER = 0.5f;
	private const float POWER_INCREASE_UNIT = 0.005f;
	private const float APPEAR_TIME = 0.8f; // 1초

	private float power;
	private bool isAppear = true;
	private bool isMouseDown = false;

	// Use this for initialization
	void Start () {
		arrowOrigin = Vector3.zero + arrow.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if (isAppear) {
			if (Input.GetMouseButtonDown (0)) {
				isMouseDown = true;
				power = POWER_INCREASE_UNIT;
			}
			if(Input.GetMouseButton(0)){
				if (isMouseDown) {
					if(power <= MAX_POWER - POWER_INCREASE_UNIT){
						power += POWER_INCREASE_UNIT;
					} 
				}
			}
		}
	}
	void Update () {
		if (isAppear) {
			if(Input.GetMouseButtonDown(0)){
				isMouseDown = true;
				power = POWER_INCREASE_UNIT;
			}
			if(Input.GetMouseButton(0)){
				if (isMouseDown) {
					if(power <= MAX_POWER - POWER_INCREASE_UNIT){
						//power += POWER_INCREASE_UNIT;
						float scale = 1 - (power / (float)MAX_POWER);
						if (scale < 0.1f) {
							scale = 0.1f;
						} else {
							Aim.transform.localScale = new Vector3(scale,scale,0);
							arrow.transform.position =(arrowOrigin-arrow.transform.forward*power/2);
						}

					} 
				}
			}
			if(Input.GetMouseButtonUp(0)){
				if (isMouseDown) {
					arrow.GetComponent<ArrowReady>().power = power+0.2f;
					arrow.GetComponent<ArrowReady>().angle = 360f - arrowPosition.eulerAngles.x;
					Instantiate(arrow,arrowPosition.position,arrowPosition.rotation);
					arrowGUI.OffActive();
					isAppear = false;
					isMouseDown = false;
					Aim.SetActive (false);
					Invoke("arrowGUIOn",APPEAR_TIME);
					power = 0;
					arrow.transform.position = arrowOrigin;
				}
			}
		} else {

		}
	}

	private void arrowGUIOn(){
		arrowGUI.OnActive ();
		Aim.transform.localScale = new Vector3 (1, 1, 0);
		Aim.SetActive (true);
		isAppear = true;
	}
	public float GetPower(){
		return power;
	}
}
