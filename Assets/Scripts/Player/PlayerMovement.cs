using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement Instance;
    
    [Header("References"), Space(5)]
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Animator _playerAnimator;

    [Header("Variables"), Space(5)]
    [SerializeField] private int _playerHealth;
    [Space(5)]
    [SerializeField] private float _lateralMovement;
    [SerializeField] private float _forwardMovement;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJumpForce;
    [Space(5)]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isMoving;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isCrouching;
    [SerializeField] private bool _isAlive;

    [Header("Rewired"), Space(5)]
    [SerializeField] private int _playerID;
    [HideInInspector] public Player _player;


    void Awake()
    {
        _player = ReInput.players.GetPlayer(_playerID);

        //if (_player == null)
        //{
        //    Instance = this;
        //}
    }
    
    void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- MOVEMENT -------------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        // Récupérer les valeurs des axes de mouvement
        _lateralMovement = _player.GetAxis("Lateral_Movement"); // Mouvement gauche/droite
        _forwardMovement = _player.GetAxis("Forward_Movement"); // Mouvement avant/arrière
        Debug.Log("Lateral : " + _lateralMovement + " | Forward : " + _forwardMovement);
        
        if (_lateralMovement != 0 || _forwardMovement != 0)
        {
            _isMoving = true;
        }
        else _isMoving = false;
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- JUMP -----------------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        if (_player.GetButtonDown("Jump"))
        {
            _playerRigidbody.AddForce(Vector3.up * _playerJumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            // Calculer la direction de déplacement en fonction de l'orientation locale
            Vector3 moveDirection = (transform.right * _lateralMovement + transform.forward * _forwardMovement).normalized * _playerSpeed;

            // Conserver la vélocité verticale pour éviter de perturber le saut ou la gravité
            moveDirection.y = _playerRigidbody.linearVelocity.y;

            // Appliquer la vélocité basée sur la direction locale et la vitesse de déplacement
            _playerRigidbody.linearVelocity = moveDirection;
        }
    }
}
