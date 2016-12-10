using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour {

	public int damage;
	public bool lasting;
	public bool isDamaged = false;

	// Use this for initialization
	void Start () {
		gameObject.tag = "enemy_attack";
	}
}
