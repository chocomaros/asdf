using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    private int serialNumber = 0;
    //no weapon = 0, no armor = 4;
    public bool isWeapon = false;
    public int HP = 0;
    public int Damage = 0;
	public int Speed;
	public float MaxPower;
	public string Name;
	public int Level = 0;
    //look

	public enum Type{Bow,Arrow};
	public Type type;

    public void setSerialNumber(int serial)
    {
        serialNumber = serial;
        switch (serialNumber)
        {
            case 0:
                isWeapon = true;
                HP = 0;
                Damage = 0;
                break;
            case 1:
                isWeapon = true;
                HP = 0;
                Damage = 25;
                break;
            case 2:
                isWeapon = true;
                HP = 2;
                Damage = 25;
                break;
            case 3:
                isWeapon = true;
                HP = 0;
                Damage = 50;
                break;
            case 4:
                isWeapon = true;
                HP = 0;
                Damage = 0;
                break;
            case 5:
                isWeapon = true;
                HP = 2;
                Damage = 0;
                break;
            case 6:
                isWeapon = true;
                HP = 4;
                Damage = 0;
                break;
            case 7:
                isWeapon = true;
                HP = 6;
                Damage = 0;
                break;
        }
    }

	public void SetLevel(int level){
		Level = level;
		Damage += (int)(level * 2);
		Speed += (int)(level * 2);
		MaxPower += (float)(level * 2);
	}

}