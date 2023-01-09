using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _pointRadius;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private UI_Controller UI;

    [SerializeField] private Collider2D _collider;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        _collider = Physics2D.OverlapCircle(_interactPoint.position, _pointRadius, _interactableMask);

        if (UI.messageActive && player.InputHandler.InteractInput)
        {
            UI.hideMessage();
            player.InputHandler.InteractInput = false;
        }

        if (_collider != null)
        {
            var interactable = _collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (!UI.messageActive) UI.showInteractPrompt(interactable.InteractionPrompt);

                if (!UI.messageActive && player.InputHandler.InteractInput)
                {
                    player.InputHandler.InteractInput = false;
                    interactable.Interact(this);
                    UI.showMessage(interactable.InteractedMessage, interactable.MessageAlignment);
                    UI.hideInteractPrompt();
                }

            }
            else
            {
                UI.hideInteractPrompt();
            }
        }
        else
        {
            UI.hideInteractPrompt();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactPoint.position, _pointRadius);
    }
}
