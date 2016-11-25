using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearAttack : IEnemyAttack {

	public float AttackDistance;
	public float AttackTime;
	private float attackingTime = 0;
	public override bool checkAttack (float distance){
		return distance < AttackDistance;
	}
	public override Enemy.State attack(Enemy enemy, GameObject target){
		Debug.Log ("attack");
		enemy.animator.SetBool ("isAttacking", true);
		attackingTime++;
		if (attackingTime > AttackTime) {
			target.GetComponent<PlayerStatus>().healthChange(-1);
			attackingTime=0;
		}
		if (Vector3.Distance (target.transform.position, enemy.transform.position) > AttackDistance) {
			enemy.animator.SetBool("isAttacking", false);
			attackingTime = 0;
			return Enemy.State.CHASE;
		}
		return Enemy.State.ATTACK;
	}

}
