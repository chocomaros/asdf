using UnityEngine;
using System.Collections;

public class ArrowMoveVector : MonoBehaviour {

	public Transform arrow;
	//public Rigidbody rbArrow;

	public float power = 0.01f;
	public float angle = 45f;

	private float gravity;

	private bool movingOn = true;
	private Vector3 vector;

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
				if(movingOn){
					arrow.parent = collider.transform;
					collider.GetComponent<Enemy>().HP -= 10;
					collider.GetComponent<Enemy>().BloodEffect(arrow.transform);
					if(collider.GetComponent<Enemy>().HP <= 0){
						collider.GetComponent<Enemy>().state = Enemy.State.DEATH;
					} else{
						collider.GetComponent<Enemy>().animator.SetTrigger("hit");
						collider.GetComponent<Enemy>().HitTurn(arrow.transform.position);
					}
					Debug.Log ("enemy");
					Destroy (gameObject,3f);
				}
            }
            else if (collider.transform.tag == "enemy_head")
            {
                if (movingOn)
                {
                    arrow.parent = collider.transform;
                   
					collider.GetComponentInParent<Enemy>().HP -= 50;
					collider.GetComponentInParent<Enemy>().BloodEffect(arrow.transform);
					if (collider.GetComponentInParent<Enemy>().HP <= 0)
                    {
						collider.GetComponentInParent<Enemy>().state = Enemy.State.DEATH;
                    }
                    else
                    {
						collider.GetComponentInParent<Enemy>().animator.SetTrigger("hit");
						collider.GetComponentInParent<Enemy>().HitTurn(arrow.transform.position);
                    }
                    Destroy(gameObject, 3f);
                }
            }else{
				Destroy (gameObject,3f);
			}
			movingOn = false;

		}
	}
	/*void OnTriggerStay(Collider collider){
		movingOn = false;
		Destroy(GameObject.Find("Arrow(Clone)"),3f);
	}*/
}
