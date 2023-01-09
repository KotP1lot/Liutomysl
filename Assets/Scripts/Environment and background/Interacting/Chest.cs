using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _chestCollider;
    [SerializeField] private int _givesCurrency = 0;
    public string InteractedMessage => $"Отримано {_givesCurrency} сахару.";
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<KeyInventory>();
        if (inventory == null) return false;

        // нарахувати сахар

        _animator.SetTrigger("open_chest");
        _chestCollider.enabled = false;

        return true;
    }
}
