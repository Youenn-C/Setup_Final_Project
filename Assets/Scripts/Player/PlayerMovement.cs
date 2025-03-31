using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- MOVEMENT -------------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        PlayerBrain.Instance._forwardMovement = PlayerBrain.Instance._player.GetAxis("ForwardMovement");
        PlayerBrain.Instance._lateralMovement = PlayerBrain.Instance._player.GetAxis("LateralMovement");

        if (PlayerBrain.Instance._forwardMovement != 0 || PlayerBrain.Instance._lateralMovement != 0)
        {
            PlayerBrain.Instance._isMoving = true;
        }
        else PlayerBrain.Instance._isMoving = false;
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- JUMP -----------------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        if (PlayerBrain.Instance._player.GetButtonDown("Jump"))
        {
            PlayerBrain.Instance._playerRigidbody.AddForce(Vector3.up * PlayerBrain.Instance._playerJumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (PlayerBrain.Instance._isMoving)
        {
            // Calculer la direction de déplacement en fonction de l'orientation locale
            Vector3 moveDirection = (transform.right * PlayerBrain.Instance._lateralMovement + transform.forward * PlayerBrain.Instance._forwardMovement).normalized * PlayerBrain.Instance._playerSpeed;

            // Conserver la vélocité verticale pour éviter de perturber le saut ou la gravité
            moveDirection.y = PlayerBrain.Instance._playerRigidbody.linearVelocity.y;

            // Appliquer la vélocité basée sur la direction locale et la vitesse de déplacement
            PlayerBrain.Instance._playerRigidbody.linearVelocity = moveDirection;
        }
        else if (!PlayerBrain.Instance._isMoving)
        {
            PlayerBrain.Instance._playerRigidbody.linearVelocity = new Vector3(0, PlayerBrain.Instance._playerRigidbody.linearVelocity.y, 0);
        }
    }
}
