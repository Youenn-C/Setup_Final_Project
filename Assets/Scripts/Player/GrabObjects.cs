using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [Header("Reference"), Space(5)]
    [SerializeField] private LayerMask _layerMaskGrabbableItem;
    [SerializeField] private GameObject _grabPoint;
    
    [Header("GrabbedObject"), Space(5)]
    private GameObject _grabbedObject;
    private Rigidbody _grabbedObjectRigidbody;
    private Collider _grabbedObjectCollider;
    
    
    [Header("Variables"), Space(5)]
    [SerializeField, Range(3,10)] private int _grabDistance;
    [Space(5)]
    [SerializeField] private bool _canGrab = true;
    [SerializeField] private bool _isGrabing;

    void Start()
    {
        
    }

    void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- GRAB OBJECT ----------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (_canGrab)
        {
            if (Physics.Raycast(PlayerBrain.Instance.cameraGameObject.transform.position, _grabPoint.transform.position - PlayerBrain.Instance.cameraGameObject.transform.position, out RaycastHit hit, _grabDistance))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GrabbableItem"))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    
                    if (PlayerBrain.Instance.player.GetButtonDown("GrabItem"))
                    {
                        _grabbedObject = hit.collider.gameObject;
                        _grabbedObjectRigidbody = _grabbedObject.GetComponent<Rigidbody>();
                        _grabbedObjectCollider = _grabbedObject.GetComponent<Collider>();
                        
                        _grabbedObjectRigidbody.useGravity = false;
                        _grabbedObjectCollider.enabled = false;
                        _isGrabing = true;
                        _canGrab = false;
                        
                        _grabbedObject.transform.SetParent(_grabPoint.transform);
                    }
                }
            }
        }

        if (_isGrabing && PlayerBrain.Instance.player.GetButtonUp("GrabItem"))
        {
            _grabbedObjectRigidbody.useGravity = true;
            _grabbedObjectCollider.enabled = true; 
            
            _isGrabing = false;
            _canGrab = true;
            
            _grabbedObject.transform.SetParent(null);
            _grabbedObject = null;
            _grabbedObjectRigidbody = null;
            _grabbedObjectCollider = null;
        }
    }
}
