using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour {

	public enum Type{HEAL,MAX_HP_UP,DAMAGE_UP};
	public Type type;
	public int stat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool DrinkPotion(PlayerStatus playerStatus){
		switch (type) {
		case Type.HEAL:
			if (playerStatus.getHealth () < playerStatus.MaxHp) {
				playerStatus.healthChange (stat);
				Destroy (gameObject);
				return true;
			} else {
				return false;
			}
		case Type.MAX_HP_UP:
			playerStatus.maxHealthChange (stat);
			Destroy (gameObject);
			return true;
		case Type.DAMAGE_UP:
			playerStatus.damageChange (stat);
			Destroy (gameObject);
			return true;
		}
		return false;
	}
}
