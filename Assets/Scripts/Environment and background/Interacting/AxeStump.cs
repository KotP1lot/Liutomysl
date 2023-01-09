using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxeStump : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _axe;
    [SerializeField] private Collider2D collider2d;
    public string InteractedMessage => "Ви підібрали сокиру.\nЛКМ - слабка атака, ПКМ - сильна атака.\n[E] - закрити вікно.";
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Left;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var player = interactor.GetComponent<Player>();

        player.weapon.SetActive(true);

        collider2d.enabled = false;
        Destroy(_axe);
        return true;
    }
}
