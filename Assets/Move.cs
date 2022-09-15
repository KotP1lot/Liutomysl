using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpforce;
    [SerializeField]
    private float _checkRadius;
    [SerializeField]
    private Transform _ground;
    [SerializeField]
    private LayerMask _groundLayer;

    private bool _isGrounded = true;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
       // _isGrounded = Physics2D.OverlapCircle(_ground.position, _checkRadius, _groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rigidbody2D.AddForce(Vector2.right * 5000);
        }
    }
    private void FixedUpdate()
    {
        if (0 != Input.GetAxis("Horizontal"))
        {
            _rigidbody2D.velocity = new Vector2(_speed * Input.GetAxis("Horizontal"), _rigidbody2D.velocity.y);

        }
        else _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            _isGrounded = true;
        }
    }
}   
