using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public GameObject player;
	public GameObject miniMapOrigin;
	public List<GameObject> RoomObjects;
	public List<GameObject> Enemies;
	// 0:None, 1:Boss, 2:room1
	private GameObject[,] floor;
	private bool isSettingEnd;
	private GameObject[,] miniMap;
	private int level = 0;

	// Use this for initialization
	void Start ()
	{
		SetFloor ();
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
		SetLevel ();

		for (int i = 0; i < RoomObjects.Count; i++) {
			RoomObjects [i].SetActive (true);
		}

		Room[,] rooms = new Room[4, 3];
		rooms [0, 0] = new Room (Room.RoomType.NONE);
		rooms [0, 1] = new Room (Room.RoomType.BOSS);
		rooms [0, 2] = new Room (Room.RoomType.NONE);
		for (int i = 1; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				rooms [i, j] = new Room (Room.RoomType.ROOM1);
			}
		}
		rooms = SetRoomPortal (rooms);

		floor = new GameObject[4, 3];
		InstantiateRoom (rooms);
		InitMap ();

		for (int i = 0; i < RoomObjects.Count; i++) {
			RoomObjects [i].SetActive (false);
		}

		floor [3, 1].GetComponent<Room> ().isPlayerHere = true;
		SetPortalActive (floor [3, 1], false);
		player.GetComponent<PlayerStatus> ().EntryPositon = Portal.Position.UP;
		MovePlayerPosition (player.GetComponent<PlayerStatus> ().EntryPositon);

		SetRoomEnable ();
	}

	GameObject GetCurrentRoom ()
	{
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (floor [i, j].GetComponent<Room> ().isPlayerHere) {
					return floor [i, j];
				}
			}
		}
		return null;
	}

	Room[,] SetRoomPortal (Room[,] rooms)
	{
		RoomGeneration roomGen = new RoomGeneration ();
		roomGen.InitRoom ();

		//Boss Room
		rooms [0, 1].connectedUp = true;
		rooms [0, 1].connectedDown = true;
		rooms [1, 1].connectedUp = true;

		for (int count = 1; count < 9; count++) {
			int a = (count / 3) + 1;
			int b = count % 3;

			if (count - roomGen.parents [count] == 1) {
				//count에 해당하는 room
				rooms [a, b].connectedLeft = true;
				rooms [a, b - 1].connectedRight = true;
				continue;
			}
			if (roomGen.parents [count] - count == 1) {
				rooms [a, b].connectedRight = true;
				rooms [a, b + 1].connectedLeft = true;
				continue;
			}
			if (count - roomGen.parents [count] == 3) {
				rooms [a, b].connectedUp = true;
				rooms [a - 1, b].connectedDown = true;
				continue;
			}
			if (roomGen.parents [count] - count == 3) {
				rooms [a, b].connectedDown = true;
				rooms [a + 1, b].connectedUp = true;
				continue;
			}
		}

		return rooms;
	}

	void InstantiateRoom (Room[,] rooms)
	{
		Vector3 mapPosition = new Vector3 (0, 0, 0);

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (i > 0) {
					int random = Random.Range (0, 0);
					switch (random) {
					case 0:
						rooms [i, j].roomType = Room.RoomType.ROOM1;
						break;
					}
				}
				switch (rooms [i, j].roomType) {
				case (Room.RoomType.ROOM1): 
					floor [i, j] = Instantiate (RoomObjects [1]);
					floor [i, j].transform.position = mapPosition;
					break;
				case (Room.RoomType.BOSS):
					floor [i, j] = Instantiate (RoomObjects [1]);
					floor [i, j].transform.position = mapPosition;
					break;
				case (Room.RoomType.NONE):
					floor [i, j] = Instantiate (RoomObjects [0]);
					break;
				}
				if (rooms [i, j].connectedDown) {
					floor [i, j].GetComponent<Room> ().connectedDown = true;
				}
				if (rooms [i, j].connectedUp) {
					floor [i, j].GetComponent<Room> ().connectedUp = true;
				}
				if (rooms [i, j].connectedLeft) {
					floor [i, j].GetComponent<Room> ().connectedLeft = true;
				}
				if (rooms [i, j].connectedRight) {
					floor [i, j].GetComponent<Room> ().connectedRight = true;
				}

				SetEnemyGeneration (floor [i, j]);

				Component[] enemies = floor [i, j].GetComponentsInChildren<Enemy> ();
				foreach (Enemy enemy in enemies) {
					enemy.SetStatus (level);
				}

				mapPosition.x += Room.mapLengthX;
			}
			mapPosition.x = 0;
			mapPosition.z -= Room.mapLengthZ;
		}
	}

	void SetRoomEnable ()
	{
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (!floor [i, j].GetComponent<Room> ().isPlayerHere) {
					floor [i, j].SetActive (false);
				} else {
					floor [i, j].SetActive (true);
					SetPortalActive (floor [i, j], false);
				}
			}
		}
	}

	public void CurrentPortalActive ()
	{
		SetPortalActive (GetCurrentRoom (), true);
	}

	void SetPortalActive (GameObject floor, bool activation)
	{
		if (activation) {
			if (floor.GetComponent<Room> ().connectedDown) {
				floor.GetComponent<Room> ().portalDown.SetActive (true);
			}
			if (floor.GetComponent<Room> ().connectedUp) {
				floor.GetComponent<Room> ().portalUp.SetActive (true);
			}
			if (floor.GetComponent<Room> ().connectedLeft) {
				floor.GetComponent<Room> ().portalLeft.SetActive (true);
			}
			if (floor.GetComponent<Room> ().connectedRight) {
				floor.GetComponent<Room> ().portalRight.SetActive (true);
			}
		} else {
			floor.GetComponent<Room> ().portalUp.SetActive (false);
			floor.GetComponent<Room> ().portalDown.SetActive (false);
			floor.GetComponent<Room> ().portalLeft.SetActive (false);
			floor.GetComponent<Room> ().portalRight.SetActive (false);
		}
	}

	void InitMap ()
	{
		miniMapOrigin.SetActive (true);
		miniMap = new GameObject[4, 3];
		Vector3 mapLocalPosition = new Vector3 (0, 0, 0);
		GameObject mapPosition = GameObject.Find ("Map Position");
		int x = 25, y = 16;
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (floor [i, j].GetComponent<Room> ().roomType != Room.RoomType.NONE) {
					miniMap [i, j] = Instantiate (miniMapOrigin, mapPosition.transform);
					miniMap [i, j].transform.localPosition = mapLocalPosition;
				}
				mapLocalPosition.x += x;
			}
			mapLocalPosition.x = 0;
			mapLocalPosition.y -= y;
		}
		miniMapOrigin.SetActive (false);
	}

	void SetMapColor ()
	{
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 3; j++) {
				if (floor [i, j].GetComponent<Room> ().roomType != Room.RoomType.NONE) {
					miniMap [i, j].GetComponent<Image> ().color = Color.black;
					if (floor [i, j].GetComponent<Room> ().isVisited) {
						miniMap [i, j].GetComponent<Image> ().color = Color.gray;
					}
					if (floor [i, j].GetComponent<Room> ().isPlayerHere) {
						miniMap [i, j].GetComponent<Image> ().color = Color.white;
					}
				}
			}
		}
	}

	void MovePlayerPosition (Portal.Position entryPosition)
	{
		bool nextLevel = false;
		if (!isSettingEnd) {
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 3; j++) {
					if (floor [i, j].GetComponent<Room> ().isPlayerHere) {
						player.transform.position = floor [i, j].GetComponent<Room> ().portalDown.transform.position;
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
								player.transform.position = floor [i, j - 1].GetComponent<Room> ().portalRight.transform.position;
								if (!floor [i, j - 1].GetComponent<Room> ().isVisited) {
									SetPortalActive (floor [i, j - 1], false);
								}
							}
							break;
						case Portal.Position.RIGHT:
							if (j < 3 - 1) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i, j + 1].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i, j + 1].SetActive (true);
								player.transform.position = floor [i, j + 1].GetComponent<Room> ().portalLeft.transform.position;
								if (!floor [i, j + 1].GetComponent<Room> ().isVisited) {
									SetPortalActive (floor [i, j + 1], false);
								}
							}
							break;
						case Portal.Position.UP:
							if (i == 0) {
								nextLevel = true;
								break;
							} else if (i > 0) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i - 1, j].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i - 1, j].SetActive (true);
								player.transform.position = floor [i - 1, j].GetComponent<Room> ().portalDown.transform.position;
								if (!floor [i - 1, j].GetComponent<Room> ().isVisited) {
									SetPortalActive (floor [i - 1, j], false);
								}
							}
							break;
						case Portal.Position.DOWN:
							if (i < 4 - 1) {
								floor [i, j].GetComponent<Room> ().isVisited = true;
								floor [i, j].GetComponent<Room> ().isPlayerHere = false;
								floor [i + 1, j].GetComponent<Room> ().isPlayerHere = true;
								floor [i, j].SetActive (false);
								floor [i + 1, j].SetActive (true);
								player.transform.position = floor [i + 1, j].GetComponent<Room> ().portalUp.transform.position;
								if (!floor [i + 1, j].GetComponent<Room> ().isVisited) {
									SetPortalActive (floor [i + 1, j], false);
								}
							}
							break;
						}
					}
				}
			}
		}
		if (!nextLevel) {
			SetMapColor ();
		} else {
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 3; j++) {
					DestroyImmediate (floor [i, j]);
					DestroyImmediate (miniMap [i, j]);
				}
			}
			SetFloor ();
		}
	}

	private void SetEnemyGeneration(GameObject room){
		int enemiesCount = Random.Range (3, 6);
		Vector3[] position = new Vector3[enemiesCount];
		int[] randPosition = new int[enemiesCount];
		for (int i = 0; i < enemiesCount; i++) {
			bool check = false;

			while (!check) {
				int random = Random.Range (0, 10);
				if (i == 0) {
					randPosition [i] = random;
					check = true;
				} else {
					for (int j = 0; j < i; j++) {
						if (random == randPosition [j]) {
							break;
						}
						if (random != randPosition [j] && j == i -1) {
							randPosition [i] = random;
							check = true;
						}
					}
				}
			}
			int x = randPosition [i] / 3;
			int z = randPosition [i] % 3;
			x = (x - 1) * 10;
			z = (z - 1) * 10;
			position [i] = new Vector3 (x, 0, z);
			GameObject enemy = Instantiate (Enemies[Random.Range(0,Enemies.Count)], new Vector3(), Quaternion.Euler(0,Random.Range(0,180),0) , room.transform);
			enemy.transform.localPosition = position [i];
		}
	}

	private void SetLevel(){
		level++;
		GameObject.Find ("Level Text").GetComponent<Text> ().text = "Floor : " + level;
	}
}
