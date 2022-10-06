using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _placeToClimb;
    [SerializeField] private Collider2D _platform;
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
