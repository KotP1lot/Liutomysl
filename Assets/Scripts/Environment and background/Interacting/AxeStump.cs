using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStump : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject _axe;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var player = interactor.GetComponent<Player>();

        player.weapon.SetActive(true);

        Destroy(_axe);
        return true;
    }
}
