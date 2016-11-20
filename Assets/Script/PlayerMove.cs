using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	private const float walkSpeed = 2.0f;
	private const float runSpeed = 4.0f;
	private const float turnSpeed = 5.0f;
	private Vector3 v3;
	public CharacterController CC;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		float speed = walkSpeed;
	
		v3 = new Vector3 (0, Input.GetAxis ("Mouse X"), 0);
		transform.Rotate (v3 * turnSpeed);

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = runSpeed;
		}
		if (Input.GetKey(KeyCode.W)) {
			Vector3 direction = gameObject.transform.forward;
			CC.Move(direction*speed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S)) {
			Vector3 direction = -gameObject.transform.forward;
			CC.Move(direction*speed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A)) {
			Vector3 direction = gameObject.transform.right;
			CC.Move(-direction*speed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D)) {
			Vector3 direction = gameObject.transform.right;
			CC.Move(direction*speed*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Escape)){

		}
	}
}
