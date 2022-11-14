using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _placeToClimb;
    [SerializeField] private Collider2D _platform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float sprite_height = 1.56f;

    private void Awake()
    {
        _platform.gameObject.transform.localPosition = new Vector3(0, 0.75f + _spriteRenderer.bounds.size.y/2 - sprite_height/2);
        _placeToClimb.localPosition = new Vector3(0, 0.75f + _spriteRenderer.bounds.size.y / 2 - sprite_height / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Move>() != null) {
            collision.gameObject.GetComponent<Move>().LadderState(true, _placeToClimb, _platform);
        //    gameObject.GetComponent<PlatformEffector2D>().colliderMask = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.GetComponent<Move>() != null)
        {
            collision.gameObject.GetComponent<Move>().LadderState(false);
            //gameObject.GetComponent<PlatformEffector2D>().colliderMask = 1;
        }
    }
}
