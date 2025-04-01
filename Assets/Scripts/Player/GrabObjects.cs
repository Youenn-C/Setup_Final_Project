using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [Header("Reference"), Space(5)]
    [SerializeField] private GameObject _grabPoint;

    void Start()
    {
        
    }

    void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- GRAB OBJECT ----------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        Ray grabRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(PlayerBrain.Instance.cameraGameObject.transform.position, PlayerBrain.Instance.playerGameObject.transform.position - _grabPoint.transform.position, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
