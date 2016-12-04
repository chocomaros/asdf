using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 {

	public bool isVisited = false;
	public bool isPlayerHere = false;
	public bool connectedUp, connectedDown, connectedLeft, connectedRight;
	public GameObject map;
	public Transform portalUp, portalDown, portalLeft, portalRight;
	public const int mapLengthX = 50, mapLengthZ = 50;

	public enum RoomType{NONE,ROOM1,BOSS};
	public RoomType roomType;

	public Room1(RoomType roomType){
		this.roomType = roomType;
		connectedUp = true;
		connectedDown = true;
		connectedLeft = true;
		connectedRight = true;
	}

	public void SetPortal(){
	}
}
