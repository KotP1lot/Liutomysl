using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public SceneTransition sceneTransition;
    public string InteractedMessage => "";
    public TextAlignmentOptions MessageAlignment => TextAlignmentOptions.Center;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var player = interactor.GetComponent<Player>();
        if (player == null) return false;

        player.SetSavePosition(transform.position);

        DataPersistenceManager.instance.SaveGame();

        sceneTransition.SleepTransition(0);

        return true;
    }
}
