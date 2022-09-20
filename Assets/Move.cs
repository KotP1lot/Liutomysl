using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpforce;
    [SerializeField]
    private Vector2 _checkSize;
    [SerializeField]
    private Transform _ground;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _timeForDash;

    private GameObject _ladder;
    private float _gravityScale;
    private bool _isLadder;
    private bool  _isFaceRight;
    private float _startDash;
    private bool _isDash;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _gravityScale = 2;
        _isLadder = false;
        _isFaceRight = true;
        _isDash = false;
        _isGrounded = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log($"{_isGrounded} | Grounded ||" +
            $"{_isDash} | Dash ||" +
            $"{_isLadder} | Ladder");

        _isGrounded = Physics2D.OverlapBox(_ground.position, _checkSize, 0, _groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isDash)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _isGrounded)
        {
            _startDash = Time.time;
            _isDash = true;
        }
        if (Time.time - _startDash > _timeForDash)
        {
            _isDash = false;
        }
    }
    private void FixedUpdate()
    {
        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        if (_isLadder)
        {
            if (verticalInputs != 0 && !_isDash)
            {
                gameObject.transform.position = new Vector2( _ladder.transform.position.x, gameObject.transform.position.y);
                
                if (verticalInputs < 0) /*Physics2D.IgnoreLayerCollision(6, 7, true);*/  GetComponent<PlatformEffector2D>().colliderMask -= LayerMask.GetMask("Player");
                else if(verticalInputs > 0) Physics2D.IgnoreLayerCollision(6, 7, false);
                
               _rigidbody2D.gravityScale = 0;
               //_rigidbody2D.isKinematic = true;
                _rigidbody2D.velocity = new Vector2(0, _speed * verticalInputs);
            }
            else if (/*_rigidbody2D.isKinematic == true*/ _rigidbody2D.gravityScale == 0) _rigidbody2D.velocity = new Vector2(0, 0);
        }
        if (horizontalInputs < 0 && _isFaceRight) FlipCharacter();
        else if (horizontalInputs > 0 && !_isFaceRight) FlipCharacter();

        if (horizontalInputs != 0 && !_isDash)
        {
            _rigidbody2D.velocity = new Vector2(_speed * horizontalInputs, _rigidbody2D.velocity.y);    
        }
        else
        {
            if (_isGrounded) _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
        if (_isDash)
        {
            _rigidbody2D.velocity = new Vector2(15 * gameObject.transform.localScale.x, _rigidbody2D.velocity.y);
        }
       
    }

    private void FlipCharacter() {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _isFaceRight = !_isFaceRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            _ladder = collision.gameObject;
            _isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
           // _rigidbody2D.isKinematic = false;
           _rigidbody2D.gravityScale = 2;
            _isLadder = false;
        }
    }
}   
