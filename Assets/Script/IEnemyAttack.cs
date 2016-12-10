using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemyAttack : MonoBehaviour {

	public virtual bool checkAttack (float distance){
		return false;
	}
	public virtual Enemy.State attack(Enemy enemy, GameObject target){
		return Enemy.State.ATTACK;
	}
}
