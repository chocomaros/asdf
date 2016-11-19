using UnityEngine;
using System.Collections;

public interface IEnemy  {

	void chasePlayer(GameObject player);
	void attack();
}
