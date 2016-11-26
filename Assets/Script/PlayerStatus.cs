using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public void healthChange(int changedHp){
		this.hp += changedHp;
		if (hp < 0) hp = 0;
		float hpRate = hp / (float)MaxHp;
		Color c = GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color;
		c.a = 0.5f - 0.5f * hpRate;
		GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color = c;
	}
	public int getHealth(){
		return hp;
	}
	public int getMaxHealth(){
		return MaxHp;
	}


}
