using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{
    /*we retrieve the position of the list of game objects (and in particular the first one)*/
    private SceneObjectsManager objectsData;
    /*the object to display - updated with the switch function*/
    public Transform objectTransform;
    /*to calculate offsets*/
    public Vector3 _cameraOffset;
    public Quaternion cameraRotation;
    /*the initial position of the camera*/
    public Vector3 startPosition;
    /*navigation speed*/
    public float rotationSpeed = 2.0f;

    /*to set vertical limits*/
    //don't use
    public float minVerticalAngle = -89.0f;
    public float maxVerticalAngle = 89.0f;

    //navigation
    public float x = 0.0f;
    public float y = 0.0f;

    //update position
    public Quaternion rotation;
    public Vector3 position;

    //check whether we are in the navigation area or the menu
    public GameObject menuUser;
    public Vector3 initialMousePosition;

    //public float RotationsSpeed = 5.0f;
    //for the collision
    public int _count;
    public bool resetPosition = false;

    private VisualizationManager visualizationManager;


    void Awake()
    {
        objectsData = FindObjectOfType<SceneObjectsManager>();
        objectTransform = objectsData.pivotObjectsSceneList[0].transform;
        _cameraOffset = transform.position - objectTransform.position;
        startPosition = _cameraOffset;
        cameraRotation = transform.rotation;
       _count = 0;
        visualizationManager = FindObjectOfType<VisualizationManager>();
    }

    void LateUpdate()
    {

        if (pauseMenu.isOn == false && menuUser.GetComponent<OnMouseOverScript>().menu == false)
        {
            visualizationManager.Navigate();
            visualizationManager.Zoom();
        }

        /*for navigation and zoom*/
        position = rotation * _cameraOffset + objectTransform.position;
        transform.rotation = rotation;
        transform.position = position;
        transform.LookAt(objectTransform);

        /*switch models*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objectTransform = objectsData.activeObjectPivot.transform;
        }
        if (objectsData.switchModeles == true)
        {
            objectTransform = objectsData.activeObjectPivot.transform;
            objectsData.switchModeles = false;
        }

        //TODO = Update here or in the object manager section? Or in a special object file?
    }



    public void OnTriggerEnter(Collider other)
    {
        ++_count;
    }

    public void OnTriggerExit(Collider other)
    {
        --_count;
    }

    public bool IsCollision()
    {
        return _count == 0;
    }

}
