using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public enum State{PATROL,CHASE,ATTACK,DEATH};

	public State state = State.PATROL;

	public float PatrolSpeed = 0.5f;
	public float ChaseSpeed = 1.0f;
	public float TurnSpeed = 2.0f;
	public float SightDistance = 10.0f;
	public float AttackDistance = 2.0f;
	public float heightMultiplier = 1.5f;

	public float PatrolTurnDistance = 7.0f;
	public int PatrolMoveTimeMin = 5;
	public int PatrolMoveTimeMax = 7;

	public float HP = 100f;

	private GameObject target;
	public Animator animator;

	private bool isAlive = true;

	private float timer;
	private int patrolMoveTime;
	private bool isTurning = false;
	private float rand_y;

	private NavMeshAgent agent;
	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		timer = 0;
		patrolMoveTime = Random.Range (PatrolMoveTimeMin, PatrolMoveTimeMax);
		StartCoroutine ("FSM");
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FSM(){
		while (isAlive) {
			switch (state) {
			case State.PATROL :
				Patrol();
				break;
			case State.CHASE :
				Chase ();
				break;
			case State.ATTACK :
				Attack();
				break;
			case State.DEATH :
				Death ();
				break;
			}
			yield return null;
		}
	}

	void Patrol(){
		animator.SetFloat ("speed", PatrolSpeed);
		if (isTurning) {
			Vector3 rotateVector = new Vector3(0,rand_y,0);
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(rotateVector),TurnSpeed*Time.deltaTime);
			Debug.Log("(0,"+rand_y+",0)" + transform.rotation.eulerAngles);
			if(AlmostEqual(transform.rotation.eulerAngles,rotateVector,1.0f)){
				isTurning = false;
			}
		} else {
			agent.speed = PatrolSpeed;
			timer += Time.deltaTime;
			agent.Move (transform.forward*Time.deltaTime);
			if (patrolMoveTime <= timer) {
				SetTurning(0,360);
			}
		}

		RaycastHit hit;
		Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, transform.forward * SightDistance, Color.green);
		Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right) * SightDistance, Color.green);
		Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right) * SightDistance, Color.green);

		if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, SightDistance)){
			if(hit.collider.tag == "Player"){
				state = State.CHASE;
				target = hit.collider.gameObject;
			} else if(hit.collider.tag == "wall"){
				if(Vector3.Distance(hit.collider.gameObject.transform.position,gameObject.transform.position) < PatrolTurnDistance && !isTurning){
					SetTurning(90,270);
				}
			}
		}
		if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward + transform.right, out hit, SightDistance)){
			if(hit.collider.tag == "Player"){
				state = State.CHASE;
				target = hit.collider.gameObject;
			}
		}
		if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward - transform.right, out hit, SightDistance)){
			if(hit.collider.tag == "Player"){
				state = State.CHASE;
				target = hit.collider.gameObject;
			}
		}
	}

	void Chase(){
		animator.SetFloat ("speed", ChaseSpeed);
		agent.speed = ChaseSpeed;
		agent.destination = target.transform.position;
		Debug.Log (target.transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.transform.position - transform.position),TurnSpeed*Time.deltaTime);
		agent.Move (transform.forward*Time.deltaTime);

		if (Vector3.Distance (target.transform.position, transform.position) < AttackDistance) {
			state = State.ATTACK;
		}
	}

	void Attack(){
		Debug.Log ("attack");
		animator.SetBool ("isAttacking", true);
		if (Vector3.Distance (target.transform.position, transform.position) > AttackDistance) {
			animator.SetBool("isAttacking", false);
			state = State.CHASE;
		}
	}

	void Death(){
		isAlive = false;
		animator.SetBool ("isAlive", false);
		Destroy (gameObject, 3f);
	}

	void SetTurning(int min, int max){
		isTurning = true;
		rand_y = Random.Range(min,max);
		patrolMoveTime = Random.Range(PatrolMoveTimeMin,PatrolMoveTimeMax);
		timer = 0;
	}

	bool AlmostEqual(Vector3 v1, Vector3 v2, float precision){
		bool equal = true;
		if (Mathf.Abs (v1.x - v2.x) > precision)
			equal = false;
		if (Mathf.Abs (v1.y - v2.y) > precision)
			equal = false;
		if (Mathf.Abs (v1.z - v2.z) > precision)
			equal = false;

		return equal;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "arrow") {
			Debug.Log(HP + "");
			animator.SetTrigger("hit");
			HP -= 50;
			if(HP <= 0){
				Debug.Log("hp 없다");
				state = State.DEATH;
			}
		}
	}
}
