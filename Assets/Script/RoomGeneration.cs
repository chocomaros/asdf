using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration
{

	int[,] rooms;
	public int[] parents;

	public RoomGeneration ()
	{
	}
	// 0 1 2
	// 3 4 5
	// 6 7 8
	public void InitRoom ()
	{
		rooms = new int[,] {
			{ 0, 1, 0, 1, 0, 0, 0, 0, 0 }, { 1, 0, 1, 0, 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 1, 0, 0, 0 },

			{ 1, 0, 0, 0, 1, 0, 1, 0, 0 }, { 0, 1, 0, 1, 0, 1, 0, 1, 0 }, { 0, 0, 1, 0, 1, 0, 0, 0, 1 },

			{ 0, 0, 0, 1, 0, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1, 0, 1, 0, 1 }, { 0, 0, 0, 0, 0, 1, 0, 1, 0 }
		};

		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < i; j++) {
				if (rooms [i, j] != 0) {
					int temp = Random.Range (1, 70);
					rooms [i, j] = temp;
					rooms [j, i] = temp;
				}
			}
		}
		parents = Prim (rooms, 9); 

	}

	public bool isConnected(int roomNum, int position){
		if (parents [roomNum] == roomNum + position) {
			return true;
		}
		for (int i = 0; i < 9; i++) {
			if (parents [i] == roomNum + position) {
				return true;
			}
		}
		return false;
	}


	private int MinKey (int[] key, bool[] set, int verticesCount)
	{
		int min = int.MaxValue, minIndex = 0;

		for (int v = 0; v < verticesCount; ++v) {
			if (set [v] == false && key [v] < min) {
				min = key [v];
				minIndex = v;
			}
		}

		return minIndex;
	}

	private bool convertBool (int value)
	{
		return value != 0;
	}

	public int[] Prim (int[,] graph, int verticesCount)
	{
		int[] parent = new int[verticesCount];
		int[] key = new int[verticesCount];
		bool[] mstSet = new bool[verticesCount];

		for (int i = 0; i < verticesCount; ++i) {
			key [i] = int.MaxValue;
			mstSet [i] = false;
		}

		key [0] = 0;
		parent [0] = -1;

		for (int count = 0; count < verticesCount - 1; ++count) {
			int u = MinKey (key, mstSet, verticesCount);
			mstSet [u] = true;

			for (int v = 0; v < verticesCount; ++v) {
				if (convertBool (graph [u, v]) && mstSet [v] == false && graph [u, v] < key [v]) {
					parent [v] = u;
					key [v] = graph [u, v];
				}
			}
		}
		Print (parent, rooms, 9);
		return parent;
	}

	private void Print (int[] parent, int[,] graph, int verticesCount)
	{
		Debug.Log ("Edge     Weight");
		for (int i = 1; i < verticesCount; ++i)
			Debug.Log (" "+(parent [i]+1) +" - "+( i+1)+" "+ graph [i, parent [i]]);
	}

}
