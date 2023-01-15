using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
    public Vector3 lastSavePosition;
    public SerializableDictionary<string, bool> objectsInteracted;
    public int upgradeCount;
    public int playerDamage;
    public float playerAtkSpd;
    public int playerMaxHP;
    public int playerMaxSP;
    public SerializableDictionary<string, bool> keyInventory;
    public bool axeAcquired;

    public GameData()
    {
        lastSavePosition = new Vector3(0,-1.2f,0);
        objectsInteracted = new SerializableDictionary<string, bool>();
        upgradeCount = 0;
        playerDamage = 0;
        playerAtkSpd = 1;
        playerMaxHP = 90;
        playerMaxSP = 40;
        keyInventory = new SerializableDictionary<string, bool>();
        keyInventory.Add("None", true);
        keyInventory.Add("Prison", false);
        keyInventory.Add("Lift", false);
        keyInventory.Add("Mason", false);
        keyInventory.Add("Boss", false);
        axeAcquired = false;
    }
}
