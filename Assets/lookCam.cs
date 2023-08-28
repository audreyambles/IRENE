using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class lookCam : MonoBehaviour
{
    public GameObject[] modeleListe;
    public Transform objetTransform;
    public Camera cam;
    //private Vector3 _cameraOffset;
    public Vector3 _cameraOffset;
    //public Vector3 sauvegardeCamera;
    public Vector3 previousPosition;
    public Vector3 navigationPosition;

    public Vector3 positionZoom;
    public Vector3 positionInitiale;

    public GameObject menuUser;

    //public float zoomValue;

    //int LayerGUI;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public float RotationsSpeed = 5.0f;
    public int _count;

    // Start is called before the first frame update
    void Awake()
    {
        modeleListe = GameObject.FindGameObjectsWithTag("Objet");
        objetTransform = modeleListe[0].transform;
        _cameraOffset = transform.position - objetTransform.position;
        positionZoom.z = _cameraOffset.z;
        positionInitiale = _cameraOffset;
        previousPosition = _cameraOffset;
        _count = 0;
        //zoomValue = 0;
        //LayerGUI = LayerMask.NameToLayer("UI");
    }


    /* A améliorer */

    public void reinitialiserPosition()
    {
        _cameraOffset = positionInitiale;
    }

    public void reinitialiserZoom()
    {
        //Camera.main.transform.position.z = transform.position.z - zoomValue;
        //Camera.main.transform.position = previousPosition;
        //previousPosition = previousPosition.z + zoomValue;
        //VectorpreviousPosition.z; 
        _cameraOffset.z = positionZoom.z;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && pauseMenu.isOn == false && menuUser.GetComponent<OnMouseOverScript>().menu==false)
        {
            Navigate();
        }

        Zoom();

        Vector3 newPos = objetTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        transform.LookAt(objetTransform);
        // click droit = réinitialiser ?

    }

    void Navigate()
    {
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
        Quaternion camUpAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, Vector3.left); //Vector3.forward ?

        _cameraOffset = (camTurnAngle * camUpAngle) * _cameraOffset;
    }

    void Zoom()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && isCollision())
        {
            _cameraOffset.z = _cameraOffset.z + 0.1f;
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _cameraOffset.z = _cameraOffset.z - 0.1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontrigg enter");
        ++_count;
    }

    void OnTriggerExit(Collider other)
    {
        --_count;
    }

    bool isCollision()
    {
        return _count == 0;
    }

}
