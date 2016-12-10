using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{

	public int MaxHp = 10;
	private int hp;
	public int Damage = 50;
	public bool isPortalMoving = false;
	public Portal.Position EntryPositon;
	private bool isPaused = false;

	// Use this for initialization
	void Start ()
	{
		hp = MaxHp;
	}

	public void healthChange (int changedHp)
	{
		if (changedHp > 0) {
			gameObject.GetComponent<SoundPlayer> ().Speak (SoundPlayer.SpeakType.Heal);
		} else if (changedHp < 0) {
			gameObject.GetComponent<SoundPlayer> ().Speak (SoundPlayer.SpeakType.Hurt);
		}

		this.hp += changedHp;
		if (hp < 0) {
			hp = 0;
			SceneManager.LoadScene (2);
		}
		if (hp > MaxHp) {
			hp = MaxHp;
		}
		float hpRate = hp / (float)MaxHp;
		Color c = GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color;
		c.a = 0.5f - 0.5f * hpRate;
		GameObject.FindGameObjectWithTag ("background").GetComponent<Image> ().color = c;
	}

	public void maxHealthChange(int changedMaxHp){
		MaxHp += changedMaxHp;
		healthChange (changedMaxHp);
	}

	public void damageChange(int changedDamage){
		gameObject.GetComponent<SoundPlayer> ().Speak (SoundPlayer.SpeakType.PowerUp);
		Damage += changedDamage;
	}

	public int getHealth ()
	{
		return hp;
	}

	public int getMaxHealth ()
	{
		return MaxHp;
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "enemy_attack") {
			if (collider.GetComponent<EnemySkill> ().lasting) {
				healthChange (-(collider.GetComponent<EnemySkill> ().damage));
			} else {
				if (!collider.GetComponent<EnemySkill> ().isDamaged) {
					collider.GetComponent<EnemySkill> ().isDamaged = true;
					healthChange (-(collider.GetComponent<EnemySkill> ().damage));
				}
			}
			Debug.Log ("enemy_attack");
			Debug.Log ("damage : " + collider.GetComponent<EnemySkill> ().damage);

		} else if (collider.tag == "potion" ||
		           collider.tag == "item_box") {
			GameObject.Find ("Active Text").GetComponent<Text> ().enabled = true;
		} else if (collider.tag == "portal") {
			if (!isPaused) {
				gameObject.GetComponent<SoundPlayer> ().Speak (SoundPlayer.SpeakType.Teleport);
			}
			GameObject.Find ("Active Text").GetComponent<Text> ().enabled = true;
		}
	}

	void OnTriggerStay (Collider collider)
	{
		if (collider.tag == "portal") {
			if (Input.GetKey (KeyCode.E)) {
				if (!isPaused) {
					isPaused = true;
					EntryPositon = collider.GetComponent<Portal> ().position;
					isPortalMoving = true;
					GameObject.Find ("Active Text").GetComponent<Text> ().enabled = false;
					Invoke ("ReleasePause", 1f);
				}
			}
		} else if (collider.tag == "potion") {
			GameObject.Find ("Active Text").GetComponent<Text> ().enabled = true;
			if (Input.GetKey (KeyCode.E)) {
				if (!isPaused) {
					isPaused = true;
					if (collider.GetComponent<Potion> ().DrinkPotion (this)) {
						GameObject.Find ("Active Text").GetComponent<Text> ().enabled = false;
					}
					Invoke ("ReleasePause", 1f);
				}
			}
		} else if (collider.tag == "item_box") {
			if (Input.GetKey (KeyCode.E)) {
				if (!isPaused) {
					isPaused = true;
					collider.GetComponent<ItemBoxScript> ().DestoryBox ();
					GameObject.Find ("Active Text").GetComponent<Text> ().enabled = false;
					Invoke ("ReleasePause", 1f);
				}

			}
		}
	}

	void OnTriggerExit (Collider collider)
	{
		 if (collider.tag == "portal" ||
			collider.tag == "potion"||
			collider.tag=="item_box") {
			GameObject.Find ("Active Text").GetComponent<Text> ().enabled = false;
		}
	}

	void ReleasePause ()
	{
		isPaused = false;
	}
}
