using UnityEngine;
using System.Collections;

public class ArrowReady : MonoBehaviour {

	public ArrowMoveVector arrowMoveVector;
	public float power, angle;

	// Use this for initialization
	void Start () {
		arrowMoveVector.power = power;
		arrowMoveVector.angle = angle;
	}
}
