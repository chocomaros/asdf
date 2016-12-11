using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentStatShow : MonoBehaviour {

	public GameObject Window;
	public Text Name;
	public Text Damage;
	public Text Speed;
	public Text MaxPower;
	public Text MaxHp;

	public void ShowWindow(Equipment player, Equipment newItem){
		Name.text = newItem.Name + " LV"+ newItem.Level;

		int changed = newItem.Damage - player.Damage;
		if (changed >= 0) {
			Damage.text = "damage : " + newItem.Damage + " +" + changed;
		} else{
			Damage.text = "damage : " + newItem.Damage + " " + changed;
		}

		changed = newItem.Speed - player.Speed;
		if (changed >= 0) {
			Speed.text = "speed : " + newItem.Speed + " +" + changed;
		} else{
			Speed.text = "speed : " + newItem.Speed + " " + changed;
		}

		float changedf = newItem.MaxPower - player.MaxPower;
		if (changedf >= 0) {
			MaxPower.text = "max power : " + newItem.MaxPower + " +" + changedf;
		} else{
			MaxPower.text = "max power : " + newItem.MaxPower + " " + changedf;
		}

		changed = newItem.HP - player.HP;
		if (changed >= 0) {
			MaxHp.text = "max hp : " + newItem.HP + " +" + changed;
		} else{
			MaxHp.text = "max hp : " + newItem.HP + " " + changed;
		}
		Window.SetActive (true);
	}

	public void QuitWindow(){
		Window.SetActive (false);
	}
}
