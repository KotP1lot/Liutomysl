using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Collider2D _platform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float sprite_height = 1.56f;

    private void Awake()
    {
        _platform.gameObject.transform.localPosition = new Vector3(0, 0.75f + _spriteRenderer.bounds.size.y/2 - sprite_height/2);
    }
}
