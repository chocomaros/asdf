using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public enum State{PATROL,CHASE,ATTACK,DEATH};

	public State state = State.PATROL;

	private const float ORIGIN_PATROL_SPEED = 0.5f;
	private const float ORIGIN_CHASE_SPEED = 1.0f;
	private const float ORIGIN_HP = 100f;

	public float PatrolSpeed = 0.5f;
	public float ChaseSpeed = 1.0f;
	public float TurnSpeed = 2.0f;
	public float SightDistance = 10.0f;
	public float heightMultiplier = 1.5f;

	public float PatrolTurnDistance = 7.0f;
	public int PatrolMoveTimeMin = 5;
	public int PatrolMoveTimeMax = 7;

	public float HP = 100f;

	public IEnemyAttack enemyAttack;
	private GameObject target;
	public Animator animator;

	private bool isAlive = true;

	private float timer;
	private int patrolMoveTime;
	private float rand_y;
	private bool isTurning = false;
	private Quaternion qua_rotation;

	public UnityEngine.AI.NavMeshAgent agent;
	private Rigidbody rigidBody;

	public GameObject blood;
	public List<GameObject> DropItems;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
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
			case State.ATTACK:
				state = enemyAttack.attack (this, target);
				//Attack();
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
			transform.rotation = Quaternion.Slerp(transform.rotation,qua_rotation,TurnSpeed*Time.deltaTime);
			if(AlmostEqual(qua_rotation,transform.rotation,0.05f)){
				isTurning = false;
			}
		} else {
			agent.speed = PatrolSpeed;
			timer += Time.deltaTime;
			agent.Move (transform.forward*Time.deltaTime);
			if (patrolMoveTime <= timer) {
				SetRandomTurning(0,360);
			}
		}

		RaycastHit hit;
		Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right) * SightDistance, Color.green);
		Debug.DrawRay (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right) * SightDistance, Color.green);
		//Debug.Log ("right : " + transform.right);
		if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, SightDistance)){
			if(hit.collider.tag == "Player"){
				state = State.CHASE;
				target = hit.collider.gameObject;
			} else if(hit.collider.tag == "wall"){
				if(hit.distance < PatrolTurnDistance && !isTurning){
					SetRandomTurning(120,240);
				}
			}
		}
		for (int i = 1; i<11; i++) {
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward + (transform.right*0.1f*i), out hit, SightDistance)){
				if(hit.collider.tag == "Player"){
					state = State.CHASE;
					target = hit.collider.gameObject;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward - transform.right*0.1f*i, out hit, SightDistance)){
				if(hit.collider.tag == "Player"){
					state = State.CHASE;
					target = hit.collider.gameObject;
				}
			}
		}
	}

	void Chase(){
		animator.SetFloat ("speed", ChaseSpeed);
		agent.speed = ChaseSpeed;
		agent.destination = target.transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.transform.position - transform.position),TurnSpeed*Time.deltaTime);
		agent.Move (transform.forward*Time.deltaTime);

		float distance = Vector3.Distance (target.transform.position, transform.position);
		if ( enemyAttack.checkAttack(distance)) {
			state = State.ATTACK;
		}
		if (distance > SightDistance) {
			state = State.PATROL;
		}
	}

	public void Death(){
		agent.Stop ();
		animator.SetTrigger ("dead");
		isAlive = false;
		int random = Random.Range (0, 4);
		Debug.Log (random);
		Debug.Log (DropItems.Count);
		if (random < DropItems.Count) {
			Instantiate (DropItems [random], transform.position, Quaternion.identity,GameObject.FindWithTag("room").transform);
		}
		if (GameObject.FindGameObjectsWithTag ("enemy").Length == 1) {
			GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ().CurrentPortalActive ();
		}
		Destroy (gameObject, 2f);
	}

	void SetRandomTurning(int min, int max){
		isTurning = true;
		rand_y = Random.Range(min,max);
		patrolMoveTime = Random.Range(PatrolMoveTimeMin,PatrolMoveTimeMax);
		qua_rotation = Quaternion.Euler (new Vector3 (0, this.transform.rotation.eulerAngles.y+rand_y, 0));
		timer = 0;
	}

	private bool AlmostEqual(Vector3 v1, Vector3 v2, float precision){
		bool equal = true;
		if (Mathf.Abs (v1.x - v2.x) > precision) {
			equal = false;
			return equal;
		}
		if (Mathf.Abs (v1.y - v2.y) > precision){
			equal = false;
			return equal;
		}
		if (Mathf.Abs (v1.z - v2.z) > precision){
			equal = false;
			return equal;
		}

		return equal;
	}

	private bool AlmostEqual(Quaternion quaternion1, Quaternion quaternion2, float precision){
		bool equal = true;
		if (Mathf.Abs (Mathf.Abs(quaternion1.x) - Mathf.Abs(quaternion2.x)) > precision) {
			equal = false;
			return equal;
		}
		if (Mathf.Abs (Mathf.Abs(quaternion1.y) - Mathf.Abs(quaternion2.y)) > precision) {
			equal = false;
			return equal;
		}
		if (Mathf.Abs (Mathf.Abs(quaternion1.z) - Mathf.Abs(quaternion2.z)) > precision) {
			equal = false;
			return equal;
		}
		if (Mathf.Abs (Mathf.Abs(quaternion1.w) - Mathf.Abs(quaternion2.w)) > precision) {
			equal = false;
			return equal;
		}

		return equal;
	}

	public void HitTurn(Vector3 hitPosition){
		isTurning = true;
		Vector3 direction;
		direction = hitPosition - transform.position;
		direction.y = 0;
		qua_rotation = Quaternion.LookRotation (direction);
		timer = 0;
	}

	public void SetStatus(int level){
		HP = (float)((1 + (level-1) * 0.3) * ORIGIN_HP);
		PatrolSpeed = (float)((1 + (level-1) * 0.3) * ORIGIN_PATROL_SPEED);
		ChaseSpeed = (float)((1 + (level-1) * 0.3) * ORIGIN_CHASE_SPEED);
	}

	public void BloodEffect(Transform transform){
		ParticleSystem blood = Instantiate(this.blood.GetComponent<ParticleSystem>(),transform.position,transform.rotation,transform);
		blood.Emit(5);
	}
}
