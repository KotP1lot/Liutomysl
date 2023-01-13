using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, IInteractable, IDataPersistence
{
    enum OpenFrom { Both, Left, Right }
    enum Keys { None, Prison, Lift, Mason, Boss }

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] OpenFrom opensFrom = OpenFrom.Both;
    [SerializeField] Keys needsKey = Keys.None;
    [SerializeField] private string _prompt;

    private string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;

    public string InteractionPrompt => _prompt;

    [SerializeField] private string id;
    private bool opened;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<KeyInventory>();
        if (inventory == null) return false;

        if (opensFrom == OpenFrom.Left && !(interactor.transform.position.x < transform.position.x))
        {
            animator.SetTrigger("door_budge");
            message = "Не відкривається з цієї сторони.";
            return false;
        }
        if (opensFrom == OpenFrom.Right && !(interactor.transform.position.x > transform.position.x))
        {
            animator.SetTrigger("door_budge");
            message = "Не відкривається з цієї сторони.";
            return false;
        }

        if (inventory.hasKey(needsKey.ToString()))
        {
            if (needsKey == Keys.None) message = "Двері відкрито";
            else message = "Використано " + inventory.getFullKeyName(needsKey.ToString());

            animator.SetBool("door_open", true);
            doorCollider.enabled = false;

            opened = true;
            DataPersistenceManager.instance.SaveGame();
            return true;
        }

        animator.SetTrigger("door_budge");
        message = "Потрібен "+ inventory.getFullKeyName(needsKey.ToString());
        return false;
    }

    public void LoadData(GameData data)
    {
        data.objectsInteracted.TryGetValue(id, out opened);
        if (opened)
        {
            animator.SetBool("door_open", true);
            doorCollider.enabled = false;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.objectsInteracted.ContainsKey(id))
        {
            data.objectsInteracted.Remove(id);
        }
        data.objectsInteracted.Add(id, opened);
    }
}
