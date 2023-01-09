using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour, IInteractable
{
    enum Keys { None, Prison, Lift, Mason, Boss }

    [SerializeField] private string _prompt;
    [SerializeField] private Keys _givesKey=Keys.None;
    [SerializeField] private int _givesCurrency = 0;
    private string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<KeyInventory>();
        if (inventory == null) return false;

        if (_givesCurrency != 0)
        {
            message = "Отримано " + _givesCurrency + " сахару"; 
        }
        if (_givesKey != Keys.None)
        {
            inventory.acquireKey(_givesKey.ToString());
            message = "Отримано " + inventory.getFullKeyName(_givesKey.ToString());
        }
        Destroy(gameObject);
        return true;
    }
}
