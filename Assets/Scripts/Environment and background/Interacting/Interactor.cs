using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _pointRadius;
    [SerializeField] private LayerMask _interactableMask;

    [SerializeField] private Collider2D _collider;

    private void Update()
    {
        _collider = Physics2D.OverlapCircle(_interactPoint.position, _pointRadius, _interactableMask);

        if (_collider != null)
        {
            var interactable = _collider.GetComponent<IInteractable>();

            if (interactable != null && Input.GetKeyDown("e"))
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactPoint.position, _pointRadius);
    }
}
