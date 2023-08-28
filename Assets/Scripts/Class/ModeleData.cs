using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModeleData", menuName = "ELISA/Modele Data")]
public class ModeleData : ScriptableObject
{
    public int id;
    // faire une classe attribut pour remplacer les string et avoir attribut : id, nom, niv précision et pour chaque, un tableau attribut pour la forme, pour la texture... ?
    public string nom;
    public string forme;
    public string textureVue;
    public string couleur;
    public string motif3D;
    public string son;
    public string signatureOd;
    public string puissanceOd;
    public string textureTouch;

    /*
    public Attribut[] nom;
    public Attribut[] forme;
    public Attribut[] textureVue;
    public Attribut[] couleur;
    public Attribut[] motif3D;
    public Attribut[] son;
    public Attribut[] signatureOd;
    public Attribut[] puissanceOd;
    public Attribut[] textureTouch;
    */
}
