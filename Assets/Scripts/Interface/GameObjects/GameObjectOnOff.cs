using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectOnOff : MonoBehaviour
{
    bool toggleBool;
    // Start is called before the first frame update
    void Start()
    {
        toggleBool = gameObject.activeInHierarchy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveOnOff()
    {
        gameObject.SetActive(!toggleBool);
        toggleBool = gameObject.activeInHierarchy;
    }
}
