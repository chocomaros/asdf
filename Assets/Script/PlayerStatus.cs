using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	public int MaxHp = 10;
	private int hp;

	// Use this for initialization
	void Start () {
		hp = MaxHp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void healthChange(int hp){
		this.hp += hp;
	}
	public int getHealth(){
		return hp;
	}
	public int getMaxHealth(){
		return MaxHp;
	}
}
