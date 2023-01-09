using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public string InteractedMessage { get; }
    public TextAlignmentOptions MessageAlignment { get; }
    public bool Interact(Interactor interactor);
}
