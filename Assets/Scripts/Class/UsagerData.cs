using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsagerData", menuName= "ELISA/Usager Data")]
public class UsagerData : ScriptableObject
{
    public int id;
    public string nom;
    public int champsVisuel;
    public string pathologie;
    public string sensibiliteLumiere;
    public string sensibiliteOmbre;
    public Color couleurPref;
    public Color couleurDeteste;
    public SignatureMalvoyantData SensPref;
}
