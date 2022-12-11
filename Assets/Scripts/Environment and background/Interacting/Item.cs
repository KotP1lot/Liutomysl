using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    enum Keys { None, Prison, Lift, Mason, Boss }

    [SerializeField] private string _prompt;
    [SerializeField] private Keys _givesKey=Keys.None;
    [SerializeField] private int _givesCurrency = 0;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<KeyInventory>();
        if (inventory == null) return false;

        if (_givesCurrency != 0)
        {
            Debug.Log("Отримано " + _givesCurrency + " сахару"); 
        }
        if (_givesKey != Keys.None)
        {
            inventory.acquireKey(_givesKey.ToString());
            Debug.Log("Отримано " + inventory.getFullKeyName(_givesKey.ToString()));
        }
        Destroy(gameObject);
        return true;
    }
}
