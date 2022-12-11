using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private Dictionary<string, bool> _keys = new Dictionary<string, bool>();
    private Dictionary<string, string> _fullKeyNames = new Dictionary<string, string>();

    private void Start()
    {
        _keys.Add("None", true);
        _keys.Add("Prison", false);
        _keys.Add("Lift", false);
        _keys.Add("Mason", false);
        _keys.Add("Boss", false);

        _fullKeyNames.Add("None", "None");
        _fullKeyNames.Add("Prison", "Ключ Тюремника");
        _fullKeyNames.Add("Lift", "Ключ від ліфту");
        _fullKeyNames.Add("Mason", "Ключ від майстерні");
        _fullKeyNames.Add("Boss", "Королівський ключ");
    }

    public bool hasKey(string name)
    {
        if (_keys.ContainsKey(name)) return _keys[name];
        return false;
    }

    public void acquireKey(string name)
    {
        if (_keys.ContainsKey(name)) _keys[name]=true;
    }

    public string getFullKeyName(string name)
    {
        if (_fullKeyNames.ContainsKey(name)) return _fullKeyNames[name];
        return null;
    }
}
