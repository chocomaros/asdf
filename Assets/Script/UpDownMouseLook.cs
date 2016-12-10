using UnityEngine;
using System.Collections;

public class UpDownMouseLook : MonoBehaviour {

	private const float turnSpeed = 5.0f;
	private Vector3 v3;

	// Update is called once per frame
	void Update () {
		v3 = new Vector3 (-Input.GetAxis ("Mouse Y"), 0, 0);
		transform.Rotate (v3 * turnSpeed);
	}
}
