using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUISelect : MonoBehaviour {

	public List<GameObject> Arrows;
	public List<GameObject> Bows;

	public void SetActiveArrow(string ArrowName){
		for (int i = 0; i < Arrows.Count; i++) {
			if (ArrowName.Equals (Arrows [i].GetComponent<Equipment> ().Name)) {
				Arrows [i].SetActive (true);
			} else {
				Arrows [i].SetActive (false);
			}
		}
	}

	public void SetActiveBow(string BowName){
		for (int i = 0; i < Bows.Count; i++) {
			if (BowName.Equals (Bows [i].GetComponent<Equipment> ().Name)) {
				Bows [i].SetActive (true);
			} else {
				Bows [i].SetActive (false);
			}
		}
	}
}
