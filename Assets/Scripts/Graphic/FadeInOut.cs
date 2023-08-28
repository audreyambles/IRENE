using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fait une animation de fade in and out selon l'état du GameObject.
//Inutile mais c'est joli.
//Il faut appeler la fonction et non disable le gameobject directement pour l'utilisation. 
public class FadeInOut : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(FadeInMaterial(0.1f));
    }
 

    public IEnumerator FadeOutMaterial(float fadeSpeed)
    {
        Renderer rend = gameObject.transform.GetComponent<Renderer>();
        Material material = rend.materials[0];
        float alphaValue = 1f;

        while (alphaValue < 0f)
        {
            Debug.Log(alphaValue);
            alphaValue -= Time.deltaTime / fadeSpeed;
            material.SetFloat("_AlphaValue", alphaValue);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    public IEnumerator FadeInMaterial(float fadeSpeed)
    {
        Renderer rend = gameObject.transform.GetComponent<Renderer>();
        Material material = rend.materials[0];
        float alphaValue = 0f;

        while (alphaValue < 1f)
        {
            alphaValue += Time.deltaTime / fadeSpeed;
            material.SetFloat("_AlphaValue", alphaValue);
            yield return null;
        }
    }
}
