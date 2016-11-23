using UnityEngine;
using System.Collections;

public class Minotaur : MonoBehaviour, IEnemy {

	public float Speed = 0.5f;
	public float TurnSpeed = 2.0f;
	public float ChaseRange = 10.0f;
	public float AttackRange = 2.0f;

	private GameObject player;
	public Animator animator;

	private Vector3 vector;
	public Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		chasePlayer (player);
	}

 	public void chasePlayer(GameObject player){
		float distance;
		distance = Vector3.Distance (player.transform.position,rigidBody.position);
		if (distance < ChaseRange) {
			animator.SetBool ("isMoving", true);
			if(distance < AttackRange){
				animator.SetBool("isAttacking", true);
				Vector3 direction = (player.transform.position - rigidBody.position);
				direction.y = 0;
				direction.Normalize ();
				transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),TurnSpeed*Time.deltaTime);
				attack();
			} else{
				animator.SetBool("isAttacking", false);
				Vector3 direction = (player.transform.position - rigidBody.position);
				direction.y = 0;
				direction.Normalize ();
				transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),TurnSpeed*Time.deltaTime);
				transform.position += transform.forward*Speed*Time.deltaTime;
				//rigidBody.velocity= new Vector3(0,0,Speed);
				//rigidBody.AddForce(rigidBody.transform.forward*Speed*Time.deltaTime));
			}
		} else {
			animator.SetBool ("isMoving",false);
		}
	}
	
	public void attack(){

	}
}
