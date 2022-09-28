using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform placeToClimb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Move>() != null) {
            collision.gameObject.GetComponent<Move>().LadderState(true, placeToClimb);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.GetComponent<Move>() != null)
        {
            collision.gameObject.GetComponent<Move>().LadderState(false);
        }
    }
}
