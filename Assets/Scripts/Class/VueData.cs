using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VueData", menuName = "ELISA/Vue Data")]
[System.Serializable]
public class VueData : StimulusData
{
    public string instruction;

    /*
     liste de tous les paramètres de la vue ?
      public int typeContours;
      public float nettete;
      public int typeLumiere;
      public bool activerDesactiverJL; ?

    les autres seraient à paramétrer dans la personnalisation des fonctionnalités visuelles (usager)
     */
}
