using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "IRENE/User Data")]
public class UserData : ScriptableObject
{
    public int id;
    //identity
    public string lastName;
    public string firstName;
    public int age;
    /*Visual parameters preferences*/
    public float contraste;
    public float lightIntensity;
    public int colorOutlines;
    public float thicknessOutlines;
    public float staticLightTemperature;
    public float staticLightIntensity;
    /*Interface preferences*/
    public int fontType;
    public int fontSize;
    public int fontStyle;
    public int colorMenu;
    public int positionMenu;
    public float thicknessBorderMenu;
    public float thicknessBorderButton;
    public float colorBackground;

}
