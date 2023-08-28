using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TouchData", menuName = "ELISA/Touch Data")]
[System.Serializable]
public class TouchData : StimulusData
{
    public string toucher;
}
