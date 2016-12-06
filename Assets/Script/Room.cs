using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

	public bool isVisited = false;
	public bool isPlayerHere = false;
	public bool connectedUp, connectedDown, connectedLeft, connectedRight;
	public GameObject portalUp, portalDown, portalLeft, portalRight;
	public const int mapLengthX = 50, mapLengthZ = 50;

	public enum RoomType{NONE,ROOM1,BOSS};
	public RoomType roomType;

	public Room(RoomType roomType){
		this.roomType = roomType;
		connectedUp = false;
		connectedDown = false;
		connectedLeft = false;
		connectedRight = false;
	}
}
