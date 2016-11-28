using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IEnemyAttack {

	public float AttackDistance;
	public float AttackTime;
	public float SkillCoolTime;
	public float SkillCastTime;
	public float SkillDistance;
	public bool isNearAttackPossible;
	public GameObject Skill;

	private float attackingTime = 0;
	private float coolTime = 0;

	public override bool checkAttack (float distance){
		return distance < SkillDistance;
	}
	public override Enemy.State attack(Enemy enemy, GameObject target){

		float distance = Vector3.Distance (target.transform.position, enemy.transform.position);

		if ( distance > SkillDistance) {
			enemy.animator.SetBool("isAttacking", false);
			return Enemy.State.CHASE;
		}

		if (isNearAttackPossible) {
			if (coolTime == 0) {
				SkillCast (enemy, target);
				return Enemy.State.ATTACK;
			} else {
				if (distance > AttackDistance) {
					enemy.agent.destination = target.transform.position;
					transform.rotation = Quaternion.Slerp (transform.rotation, 
						Quaternion.LookRotation (target.transform.position - transform.position), enemy.TurnSpeed * Time.deltaTime);
					enemy.agent.Move (transform.forward * Time.deltaTime);
					return Enemy.State.ATTACK;
				} else {
					enemy.animator.SetBool("isAttacking", true);
					attackingTime++;
					if (attackingTime > AttackTime) {
						target.GetComponent<PlayerStatus>().healthChange(-1);
						attackingTime=0;
					}
					return Enemy.State.ATTACK;
				}
			}
		} else {
			if (coolTime == 0) {
				SkillCast (enemy, target);
				return Enemy.State.ATTACK;
			} else {
				if (distance < SkillDistance - 2) {
					enemy.agent.destination = target.transform.position;
					transform.rotation = Quaternion.Slerp (transform.rotation, 
						Quaternion.LookRotation (target.transform.position - transform.position), enemy.TurnSpeed * Time.deltaTime);
					enemy.agent.Move (-enemy.transform.forward * Time.deltaTime);
				}
				return Enemy.State.ATTACK;
			}
		}
	}

	private void SkillCast(Enemy enemy, GameObject target){
		enemy.agent.Stop ();
		enemy.animator.SetTrigger ("skill");
		coolTime = SkillCoolTime;
		StartCoroutine ("CoolTimeReset");
		StartCoroutine ("SkillUse",target);
	}

	private IEnumerator SkillUse(GameObject target){
		Vector3 skillPosition = target.transform.position;
		skillPosition.y = 0;
		yield return new WaitForSeconds (SkillCastTime);
		setSkillDamage (2);
		Destroy(Instantiate (Skill,skillPosition,new Quaternion(0,0,0,0)),2f);
	}

	private IEnumerator CoolTimeReset(){
		yield return new WaitForSeconds (SkillCoolTime);
		coolTime = 0;
	}

	private void setSkillDamage(int damage){
		Skill.GetComponent<EnemySkill> ().damage = damage;
	}
}
