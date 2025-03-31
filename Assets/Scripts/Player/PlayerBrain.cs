using UnityEngine;
using Rewired;

public class PlayerBrain : MonoBehaviour
{
    public static PlayerBrain Instance;
    
    [Header("References"), Space(5)]
    public Rigidbody _playerRigidbody;
    public Animator _playerAnimator;
    public GameObject _playerGameObject;
    public GameObject _cameraGameObject;
    
    
    [Header("Variables PlayerMovement"), Space(5)]
    public int _playerHealth;
    [Space(5)]
    public float _lateralMovement;
    public float _forwardMovement;
    public float _playerSpeed;
    public float _playerJumpForce;
    [Space(5)]
    public bool _isGrounded;
    public bool _isMoving;
    public bool _isJumping;
    public bool _isCrouching;
    public bool _isAlive;
    
    [Header("Variables CameraRotation"), Space(5)]
    public float _rotationOnX;
    public float _rotationOnY;
    [Range(0f, 1f)]
    public float _sensibility = 0.5f; // Sensibilité
    [Space(5)]
    public bool _useHorizontalCameraRotation = true;
    public bool _useVerticalCameraRotation = true;
    [Space(5)]
    [Range(-75f, 0)] public float _limitVerticalCameraRotationMin = -45f; // Limite minimale
    [Range(0, 75f)] public float _limitVerticalCameraRotationMax = 45f;  // Limite maximale
    [HideInInspector] public float _currentVerticalRotation;
    
    [Header("Rewired"), Space(5)]
    public int _playerID;
    public Player _player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
}
