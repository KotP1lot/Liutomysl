using UnityEngine;

public class Move : MonoBehaviour
{


    [SerializeField]
    private Vector2 _checkSize;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float _timeForDash;

    private GameObject _ladder;
    private float _gravityScale;
    private float _startDash;
    private Transform _ladderTransform;
    private Collider2D _ladderCollider;
    #region Forces
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpforce;
    [SerializeField]
    private float _dashforce;
    #endregion

    #region Bools
    private bool _isLadder;
    private bool _isFaceRight;
    private bool _isDash;
    private bool _isGrounded;
    private bool _isOnLadder;
    #endregion

    #region PlayersComponents
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private Transform _ground;
    #endregion

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
        // Debug.Log($"{_isGrounded} | Grounded ||" + $"{_isDash} | Dash ||" + $"{_isLadder} | Ladder");


        _isGrounded = Physics2D.OverlapBox(_ground.position, _checkSize, 0, _groundLayer);
        _isOnLadder = Physics2D.OverlapBox(_ground.position, _checkSize, 0, LayerMask.NameToLayer("Ladder"));
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isDash)
        {
            Jump();
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
        if (_isOnLadder && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log('S');
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), _ladderCollider, true);
        };
    }
    private void FixedUpdate()
    {
      
        float verticalInputs = Input.GetAxis("Vertical");

        if (_isLadder)
        {
           MoveOnLadder(verticalInputs);
        }

        float horizontalInputs = Input.GetAxis("Horizontal");
       
        if (horizontalInputs < 0 && _isFaceRight) FlipCharacter();
        else if (horizontalInputs > 0 && !_isFaceRight) FlipCharacter();
        
        MoveOnGround(horizontalInputs);

        if (_isDash)
        {
            _rigidbody2D.velocity = new Vector2(_dashforce * gameObject.transform.localScale.x, _rigidbody2D.velocity.y);
        }
       
    }

    private void MoveOnGround(float horizontalInputs)
    {
        if (horizontalInputs != 0 && !_isDash) {
            _rigidbody2D.velocity = new Vector2(_speed * horizontalInputs, _rigidbody2D.velocity.y);
        }
        else
        {
            if (_isGrounded) _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }
    private void MoveOnLadder(float verticalInputs) 
    {
        if (verticalInputs != 0 && !_isDash)
        {
            gameObject.transform.position = new Vector2(_ladderTransform.position.x, gameObject.transform.position.y);

            //if (verticalInputs < 0) Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ladder"), true);
            //else if (verticalInputs > 0) Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ladder"), true);
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = new Vector2(0, _speed * verticalInputs);
        }
        else if (_rigidbody2D.gravityScale == 0) _rigidbody2D.velocity = new Vector2(0, 0);
    }
    private void Jump()  
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpforce, ForceMode2D.Impulse);
        _isGrounded = false;
    }
    private void FlipCharacter() {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        _isFaceRight = !_isFaceRight;
    }

    public void LadderState(bool _isLadder, Transform placeToClimbe = null, Collider2D collider2D = null) {
        this._isLadder = _isLadder;
        _rigidbody2D.gravityScale = _isLadder ? _gravityScale : 2;
        _ladderTransform = placeToClimbe;
        _ladderCollider = collider2D;
    }
}   
