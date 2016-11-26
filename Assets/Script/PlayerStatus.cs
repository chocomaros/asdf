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
		int hpPercent = (int)(hp / (float)MaxHp * 100);
		Debug.Log (hpPercent);
		Color c = GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color;
		if (hpPercent <= 20) {
			c.a = 0.5f;
		} else if (hpPercent > 20 && hpPercent <= 40) {
			c.a = 0.4f;
		} else if (hpPercent > 40 && hpPercent <= 60) {
			c.a = 0.3f;
		} else if (hpPercent > 60 && hpPercent <= 80) {
			c.a = 0.2f;
		} else if (hpPercent > 80 && hpPercent < 100) {
			c.a = 0.1f;
		} else {
			c.a = 0.0f;
		}
		GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color = c;
	}
	public int getHealth(){
		return hp;
	}
	public int getMaxHealth(){
		return MaxHp;
	}


}
