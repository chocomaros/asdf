using UnityEngine;
using System.Collections;

public class Minotaur : MonoBehaviour, IEnemy {

	private const float SPEED = 0.5f;
	private const float TURN_SPEED = 2.0f;
	private const float CHASE_RANGE = 10.0f;
	private const float ATTACK_RANGE = 2.0f;

	private GameObject player;
	public Animator animator;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Capsule");
	}
	
	// Update is called once per frame
	void Update () {
		chasePlayer (player);
	}

 	public void chasePlayer(GameObject player){
		float distance;
		distance = Vector3.Distance (player.transform.position,transform.position);
		if (distance < CHASE_RANGE) {
			animator.SetBool ("isMoving", true);
			if(distance < ATTACK_RANGE){
				animator.SetBool("isAttacking", true);
				Vector3 direction = (player.transform.position - this.transform.position);
				direction.Normalize ();
				transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),TURN_SPEED*Time.deltaTime);
				attack();
			} else{
				animator.SetBool("isAttacking", false);
				Vector3 direction = (player.transform.position - this.transform.position);
				direction.Normalize ();
				transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),TURN_SPEED*Time.deltaTime);
				transform.position += transform.forward*SPEED*Time.deltaTime;
			}
		} else {
			animator.SetBool ("isMoving",false);
		}
	}
	
	public void attack(){

	}
}
