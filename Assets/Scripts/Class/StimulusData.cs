using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StimulusData", menuName = "ELISA/Stimulus Data")]
[System.Serializable]
public class StimulusData : ScriptableObject
{
    public int id;
    public string nom;
    public string attribut;
    public string valeur;
    //public float precision;
}
