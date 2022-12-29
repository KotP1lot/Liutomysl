using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _chestCollider;
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

        _animator.SetTrigger("open_chest");
        _chestCollider.enabled = true;

        return true;
    }
}
