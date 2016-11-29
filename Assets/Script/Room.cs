using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {

	public bool isVisited = false;
	public bool isPlayerHere = false;
	public bool connectedUp, connectedDown, connectedLeft, connectedRight;
	public GameObject map;

	public enum RoomType{NONE,ROOM1,BOSS};
	public RoomType roomType;

	public Room(RoomType roomType){
		this.roomType = roomType;
		connectedUp = true;
		connectedDown = true;
		connectedLeft = true;
		connectedRight = true;
	}
}
