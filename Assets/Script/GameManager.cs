using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject player;
	private GameObject[,] floor;
	private bool isSettingEnd;

	// Use this for initialization
	void Start ()
	{
		SetFloor ();
		SetRoomEnable ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player.GetComponent<PlayerStatus> ().isPortalMoving) {
			MovePlayerPosition (player.GetComponent<PlayerStatus> ().EntryPositon);
			player.GetComponent<PlayerStatus> ().isPortalMoving = false;
		}
	}

	void SetFloor ()
	{
		Debug.Log ("setFloor");
		isSettingEnd = false;

		Room[,] rooms = new Room[4, 3];
		rooms [0, 0] = new Room (Room.RoomType.NONE);
		rooms [0, 1] = new Room (Room.RoomType.BOSS);
		rooms [0, 2] = new Room (Room.RoomType.NONE);
		for (int i = 1; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				rooms [i, j] = new Room (Room.RoomType.ROOM1);
			}
		}
		floor = new GameObject[4, 3];
		Vector3 mapPosition = new Vector3 (0, 0, 0);

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				switch (rooms [i, j].roomType) {
				case (Room.RoomType.ROOM1): 
					floor [i, j] = Instantiate (GameObject.Find ("Room1"));
					floor [i, j].transform.position = mapPosition;
					break;
				case (Room.RoomType.BOSS):
					floor [i, j] = Instantiate (GameObject.Find ("Room1"));
					floor [i, j].transform.position = mapPosition;
					break;
				case (Room.RoomType.NONE):
					floor [i, j] = Instantiate (GameObject.Find ("RoomNone"));
					floor [i, j].GetComponent<Room> ().connectedDown = false;
					floor [i, j].GetComponent<Room> ().connectedLeft = false;
					floor [i, j].GetComponent<Room> ().connectedRight = false;
					floor [i, j].GetComponent<Room> ().connectedUp = false;
					break;
				}
				mapPosition.x += Room.mapLengthX;
			}
			mapPosition.x = 0;
			mapPosition.z -= Room.mapLengthZ;
		}
		GameObject.Find ("Room1").SetActive (false);
		GameObject.Find ("RoomNone").SetActive (false);
		floor [3, 1].GetComponent<Room> ().isPlayerHere = true;
		player.GetComponent<PlayerStatus> ().EntryPositon = Portal.Position.UP;
		MovePlayerPosition (player.GetComponent<PlayerStatus> ().EntryPositon);
	}
	/*
	public Room GetCurrentRoom(){
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (floor [i,j].GetComponent<Room>().isPlayerHere) {
					return floor [i,j];
				}
			}
		}
		return null;
	}*/

	void SetRoomEnable ()
	{
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (!floor [i, j].GetComponent<Room> ().isPlayerHere) {
					floor [i, j].SetActive (false);
				} else {
					floor [i, j].SetActive (true);
				}
			}
		}
	}

	void MovePlayerPosition (Portal.Position entryPosition)
	{
		if (!isSettingEnd) {
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 3; j++) {
					if (floor [i, j].GetComponent<Room> ().isPlayerHere) {
						player.transform.position = floor [i, j].GetComponent<Room> ().portalDown.position;
					}
				}
			}
			isSettingEnd = true;
		} else {
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 3; j++) {
					if (floor [i, j].GetComponent<Room> ().isPlayerHere) {
						switch (entryPosition) {
						case Portal.Position.LEFT:
							if (j > 0) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i, j - 1].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i, j - 1].SetActive (true);
								player.transform.position = floor [i, j - 1].GetComponent<Room> ().portalRight.position;
							}
							break;
						case Portal.Position.RIGHT:
							if (j < 3 - 1) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i, j + 1].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i, j + 1].SetActive (true);
								player.transform.position = floor [i, j + 1].GetComponent<Room> ().portalLeft.position;
							}
							break;
						case Portal.Position.UP:
							if (i > 0) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i - 1, j].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i - 1, j].SetActive (true);
								player.transform.position = floor [i - 1, j].GetComponent<Room> ().portalDown.position;
							}
							break;
						case Portal.Position.DOWN:
							if (i < 4 - 1) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i + 1, j].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i + 1, j].SetActive (true);
								player.transform.position = floor [i + 1, j].GetComponent<Room> ().portalUp.position;
							}
							break;
						}
					}
				}
			}
		}

	}
}
