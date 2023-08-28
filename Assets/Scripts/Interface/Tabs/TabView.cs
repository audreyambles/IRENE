using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Necessaire a ajouter a un gameobject pour le considerer comme un tabWindow. 
/// </summary>
public class TabView : MonoBehaviour
{
    public List<UnityEvent> onEnter;
    public List<Button> buttons;
}
