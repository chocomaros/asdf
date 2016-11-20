using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour {

	public float angle = 45f;
	public float power = 0.05f;

	private float timeDir = Time.deltaTime;
	private float gravity;

	private bool movingOn = true;
	private Vector3 vector;

	// Use this for initialization
	void Start () {
		gravity = -(0.9888f * timeDir * timeDir / 3.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (movingOn) {
			timeDir += Time.deltaTime;
			vector.z = power * Mathf.Cos(angle * Mathf.PI / 180f) * timeDir;
			vector.y = power * Mathf.Sin(angle * Mathf.PI / 180f) * timeDir - gravity;
			transform.Translate(vector);
			transform.Rotate(new Vector3(Mathf.Cos(angle * Mathf.PI / 180f),0,0));
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag != "Capsule") {
			movingOn = false;
			Destroy (gameObject);
		}
	}
}
			             