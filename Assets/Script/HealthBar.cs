using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	public GameObject health_full, health_half,health_none;
	public GameObject player_status;

	private GameObject[][] heart;
	// Use this for initialization
	void Start ()
	{
		PlayerStatus status = player_status.GetComponent<PlayerStatus> ();
		int maxHealth = status.getMaxHealth ();
		int health = status.getHealth ();
		Vector3 heartPosition = this.transform.position;
		for (int i = 0; i < maxHealth; i+=2) {
			if (health-i-2 >= 2) {
				Instantiate (health_full, heartPosition, this.transform.rotation, this.transform);
			} else if (health - i-2== 1 ) {
				Instantiate (health_half, heartPosition, this.transform.rotation, this.transform);
			} else {
				Instantiate (health_none, heartPosition, this.transform.rotation, this.transform);
			}
			health -= 1;
			heartPosition += new Vector3 (35, 0, 0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


}

