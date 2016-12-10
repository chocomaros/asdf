using UnityEngine;
using System.Collections;

public class ArrowMoveVector : MonoBehaviour {

	public Transform arrow;

	public float power = 0.01f;
	public float angle = 45f;

	private float gravity;

	private bool movingOn = true;
	private Vector3 vector;

	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (movingOn && gameObject.name == "Arrow(Clone)") {
			gravity = -(0.9888f * Time.deltaTime * Time.deltaTime / 3.5f);
			vector.z = power - gravity;
			vector.y = gravity;
			arrow.Translate(vector);
			arrow.Rotate (new Vector3(Mathf.Cos(angle * Mathf.PI / 180f),0,0));
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.transform.tag != "Player") {
			if (collider.transform.tag == "enemy") {
				if (movingOn) {
					arrow.parent = collider.transform;
					collider.GetComponent<Enemy> ().HP -= player.GetComponent<PlayerStatus>().Damage * power;
					collider.GetComponent<Enemy> ().BloodEffect (arrow.transform);
					if (collider.GetComponent<Enemy> ().HP <= 0) {
						collider.GetComponent<Enemy> ().state = Enemy.State.DEATH;
						//요 아래가 시체통과
						collider.GetComponent<Rigidbody> ().isKinematic = true;
						collider.GetComponent<Rigidbody> ().useGravity = false;
						Collider[] colliders = collider.GetComponentsInChildren<Collider>();

						for (int collider_i = 0; collider_i < colliders.Length;collider_i++) {
							colliders [collider_i].isTrigger = true;
						}
						colliders = collider.GetComponents<Collider>();
						for (int collider_i = 0; collider_i < colliders.Length; collider_i++) {
							colliders [collider_i].isTrigger = true;
						}
					} else {
						collider.GetComponent<Enemy> ().animator.SetTrigger ("hit");
						collider.GetComponent<Enemy> ().HitTurn (arrow.transform.position);

					}
					Debug.Log ("enemy");

					Destroy (gameObject, 3f);
				}
			} else if (collider.transform.tag == "enemy_head") {
				if (movingOn) {
					arrow.parent = collider.transform;
					
					collider.GetComponentInParent<Enemy> ().HP -= 2 * player.GetComponent<PlayerStatus>().Damage * power;
					collider.GetComponentInParent<Enemy> ().BloodEffect (arrow.transform);
					if (collider.GetComponentInParent<Enemy> ().HP <= 0) {
						collider.GetComponentInParent<Enemy> ().state = Enemy.State.DEATH;
						//요 아래가 시체통과
						collider.GetComponentInParent<Rigidbody> ().isKinematic = true;
						collider.GetComponentInParent<Rigidbody> ().useGravity = false;
						Collider[] colliders = 
							collider.GetComponentInParent<Rigidbody> ().GetComponentsInChildren<Collider>();
						for (int collider_i = 0; collider_i < colliders.Length; collider_i++) {
							colliders [collider_i].isTrigger = true;
						}
						colliders = collider.GetComponentInParent<Rigidbody> ().GetComponents<Collider>();
						for (int collider_i = 0; collider_i < colliders.Length; collider_i++) {
							colliders [collider_i].isTrigger = true;
						}
					} else {
						collider.GetComponentInParent<Enemy> ().animator.SetTrigger ("hit");
						collider.GetComponentInParent<Enemy> ().HitTurn (arrow.transform.position);
					}
					Debug.Log ("enemy head");

					Destroy (gameObject, 3f);
				}
			} else if (collider.transform.tag == "enemy_attack") {

			}else{
				Destroy (gameObject,3f);
			}
			movingOn = false;

		}
	}
}
