﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour {

	public int damage;

	// Use this for initialization
	void Start () {
		gameObject.tag = "enemy_attack";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
