using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private Player player;
	private Room[,] floor;

	// Use this for initialization
	void Start () {
		SetFloor ();
		SetRoomEnable ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetFloor(){
		Debug.Log ("setFloor");
		floor = new Room[4,3];
		floor [0,0] = new Room (Room.RoomType.NONE);
		floor [0,1] = new Room (Room.RoomType.BOSS);
		floor [0,2] = new Room (Room.RoomType.NONE);
		for (int i = 1; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				floor [i,j] = new Room (Room.RoomType.ROOM1);
			}
		}
		floor [3,1].isPlayerHere = true;

		Vector3 mapPosition = new Vector3 (0, 0, 0);

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				switch (floor [i,j].roomType) {
				case (Room.RoomType.ROOM1): 
					floor [i,j].map = Instantiate(GameObject.Find ("Room1"));
					floor [i,j].map.transform.position = mapPosition;
					break;
				case (Room.RoomType.BOSS):
					floor [i,j].map = Instantiate(GameObject.Find ("Room1"));
					floor [i,j].map.transform.position = mapPosition;
					break;
				case (Room.RoomType.NONE):
					floor [i,j].connectedDown = false;
					floor [i,j].connectedLeft = false;
					floor [i,j].connectedRight = false;
					floor [i,j].connectedUp = false;
					break;
				}
				mapPosition.x += 50;
			}
			mapPosition.x = 0;
			mapPosition.z += 50;
		}
		//GameObject.Find ("Room1").SetActive (false);
	}

	public Room GetCurrentRoom(){
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (floor [i,j].isPlayerHere) {
					return floor [i,j];
				}
			}
		}
		return null;
	}

	void SetRoomEnable(){
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (!floor [i, j].isPlayerHere) {
					if (floor [i, j].roomType != Room.RoomType.NONE) {
						Debug.Log (floor [i, j].roomType);
						floor [i, j].map.SetActive (false);
					}
				}
			}
		}
	}
}
