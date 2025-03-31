using UnityEngine;
using Rewired;

public class CameraRotation : MonoBehaviour
{ 
    void Awake()
    {
        PlayerBrain.Instance._player = ReInput.players.GetPlayer(PlayerBrain.Instance._playerID);
    }
    
    void Update()
    {
        PlayerBrain.Instance._rotationOnX = PlayerBrain.Instance._player.GetAxis("RotateOnX");
        PlayerBrain.Instance._rotationOnY = PlayerBrain.Instance._player.GetAxis("RotateOnY");
        
        if (PlayerBrain.Instance._useHorizontalCameraRotation)
        {
            PlayerBrain.Instance._playerGameObject.transform.eulerAngles += Vector3.up * (PlayerBrain.Instance._rotationOnY * PlayerBrain.Instance._sensibility);
        }
    
        if (PlayerBrain.Instance._useVerticalCameraRotation)
        {
            PlayerBrain.Instance._currentVerticalRotation += (PlayerBrain.Instance._rotationOnX * PlayerBrain.Instance._sensibility);
            PlayerBrain.Instance._currentVerticalRotation = Mathf.Clamp(PlayerBrain.Instance._currentVerticalRotation, PlayerBrain.Instance._limitVerticalCameraRotationMin, PlayerBrain.Instance._limitVerticalCameraRotationMax);

            // Appliquer la rotation avec clamp
            PlayerBrain.Instance._cameraGameObject.transform.localEulerAngles = new Vector3(-PlayerBrain.Instance._currentVerticalRotation, 0f, 0f);
        }
        
    }
}

