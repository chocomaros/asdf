using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	public GameObject health_full, health_half,health_none;
	public GameObject player;

	private PlayerStatus status;
	private int maxHealth, health;

	// Use this for initialization
	void Start ()
	{
		status = player.GetComponent<PlayerStatus> ();
		maxHealth = status.getMaxHealth ();
		health = status.getHealth ();
		drawHp ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (status.getHealth () != health || status.getMaxHealth () != maxHealth) {
			int heartNum = Mathf.CeilToInt (maxHealth / (float)2);
			for (int i = 0; i < heartNum; i++) {
				DestroyImmediate(GameObject.FindWithTag("heart"));
				Debug.Log (i);
			}
			health = status.getHealth ();
			maxHealth = status.getMaxHealth ();
			drawHp ();
		}
	}

	private void drawHp(){
		Vector3 heartPosition = this.transform.position;

		for (int i = 0; i < maxHealth; i+=2) {
			if (i == health - 1) {
				Instantiate (health_half, heartPosition, this.transform.rotation, this.transform);
			} else {
				if (i < health) {
					Instantiate (health_full, heartPosition, this.transform.rotation, this.transform);
				} else {
					Instantiate (health_none, heartPosition, this.transform.rotation, this.transform);
				}
			}
			if (i % 10 == 0 && i != 0) {
				heartPosition.y -= 35;
				heartPosition.x = transform.position.x;
			} else {
				heartPosition.x += 35;
			}
		}

	}
}

