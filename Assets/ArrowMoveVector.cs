using UnityEngine;
using System.Collections;

public class ArrowMoveVector : MonoBehaviour {

	public Transform arrow;

	public float power = 0.01f;
	public float angle = 45f;

	private float timeDir = Time.deltaTime;
	private float gravity;

	private bool movingOn = true;
	private Vector3 vector;

	// Use this for initialization
	void Start () {
		gravity = -(0.9888f * timeDir * timeDir / 3.5f);
		Destroy (GameObject.Find("Elven Long Bow Arrow(Clone)"), 6f);
	}
	
	// Update is called once per frame
	void Update () {
		if (movingOn) {
			timeDir += Time.deltaTime;
			vector.z = power - gravity;
			vector.y = gravity;
			arrow.Translate(vector);

			arrow.Rotate (new Vector3(Mathf.Cos(angle * Mathf.PI / 180f),0,0));
		}
	}

	void OnTriggerEnter(Collider collider){
		movingOn = false;
		Destroy (GameObject.Find("Elven Long Bow Arrow(Clone)"));
	}
	void OnTriggerStay(Collider collider){
		movingOn = false;
		Destroy(GameObject.Find("Elven Long Bow Arrow(Clone)"));
	}
}
