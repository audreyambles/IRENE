using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SignatureMalvoyantData" , menuName = "ELISA/Signature Malvoyant Data")]
public class SignatureMalvoyantData : ScriptableObject
{
    public int id;
    public float scoreTotalVue;
    public float scoreForme;
    public float scoreColor;
    public float scoreAspectTexture;
    public float scoreMotif3D;
    public float scoreTotalOdorat;
    public float scoreSignatureOdeur;
    public float scoreIntensiteOdeur;
    public float scoreTotalOuie;
    public float scoreSon;
    public float scoreTotalToucher;
    public float scoreTextureAsperiteSurface;
}
