using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerUpdate : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    
    public Color ReturnColorPicker()
    {
        return fcp.color;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
