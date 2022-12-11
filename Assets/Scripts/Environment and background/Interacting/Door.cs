using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    enum OpenFrom { Both, Left, Right }
    enum Keys { None, Prison, Lift, Mason, Boss }

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] OpenFrom opensFrom = OpenFrom.Both;
    [SerializeField] Keys needsKey = Keys.None;
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<KeyInventory>();
        if (inventory == null) return false;

        if (opensFrom == OpenFrom.Left && !(interactor.transform.position.x < transform.position.x))
        {
            animator.SetTrigger("door_budge");
            Debug.Log("�� ����������� � ���� �������.");
            return false;
        }
        if (opensFrom == OpenFrom.Right && !(interactor.transform.position.x > transform.position.x))
        {
            animator.SetTrigger("door_budge");
            Debug.Log("�� ����������� � ���� �������.");
            return false;
        }

        if (inventory.hasKey(needsKey.ToString()))
        {
            if (needsKey == Keys.None) Debug.Log("���� �������");
            else Debug.Log("����������� " + inventory.getFullKeyName(needsKey.ToString()));

            animator.SetBool("door_open", true);
            doorCollider.enabled = false;
            return true;
        }

        animator.SetTrigger("door_budge");
        Debug.Log("������� "+ inventory.getFullKeyName(needsKey.ToString()));
        return false;
    }
}
