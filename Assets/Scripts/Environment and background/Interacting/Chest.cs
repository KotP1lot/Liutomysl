using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D chestCollider;
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

        animator.SetTrigger("open_chest");
        chestCollider.enabled = true;

        return true;
    }
}
