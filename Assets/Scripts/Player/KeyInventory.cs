using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour, IDataPersistence
{
    private SerializableDictionary<string, bool> _keys = new SerializableDictionary<string, bool>();
    private Dictionary<string, string> _fullKeyNames = new Dictionary<string, string>();
    private Player _player;
    private void Start()
    {
        _player = GetComponent<Player>();
        _fullKeyNames.Add("None", "None");
        _fullKeyNames.Add("Prison", "���� ���������");
        _fullKeyNames.Add("Lift", "���� �� ����");
        _fullKeyNames.Add("Mason", "���� �� ��������");
        _fullKeyNames.Add("Boss", "����������� ����");
    }
    public static Action getKey;
    public bool hasKey(string name)
    {
        if (_keys.ContainsKey(name)) return _keys[name];
        return false;
    }

    public void acquireKey(string name)
    {
        _player.playKeySound();
        if (_keys.ContainsKey(name)) _keys[name]=true;
    }

    public string getFullKeyName(string name)
    {
        if (_fullKeyNames.ContainsKey(name)) return _fullKeyNames[name];
        return null;
    }

    public void LoadData(GameData data)
    {
        _keys = data.keyInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.keyInventory = _keys;
    }
}
