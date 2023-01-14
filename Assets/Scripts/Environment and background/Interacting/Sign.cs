using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    [TextArea]
    public string message;
    public string InteractedMessage => message;
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Left;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        return true;
    }
}
