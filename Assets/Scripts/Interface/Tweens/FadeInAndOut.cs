using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn()
    {
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            gameObject.GetComponent<Image>().color = t.CurrentValue;
        };

        Color endAlpha = gameObject.GetComponent<Image>().color;
        endAlpha.a = 1;

        // completion defaults to null if not passed in
        gameObject.Tween("ColorCircle", gameObject.GetComponent<Image>().color, endAlpha, 0.5f, TweenScaleFunctions.QuadraticEaseOut, updateColor);

    }
    public void FadeOut()
    {
        Debug.Log(gameObject.name);
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            gameObject.GetComponent<Image>().color = t.CurrentValue;
        };

        System.Action<ITween<Color>> ColorCompleted = (t) =>
        {
            gameObject.SetActive(false);
        };
        Color endAlpha = gameObject.GetComponent<Image>().color;
        endAlpha.a = 0;

        // completion defaults to null if not passed in

        gameObject.Tween("ColorCircle", gameObject.GetComponent<Image>().color, endAlpha, 0.5f, TweenScaleFunctions.QuadraticEaseIn, updateColor, ColorCompleted);

    }

    public void FadeInCanvas()
    {
        Debug.Log(gameObject.name);
        System.Action<ITween<float>> updateColor = (t) =>
        {
            gameObject.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        CanvasGroup g = gameObject.GetComponent<CanvasGroup>();
        g.alpha = 1;

        // completion defaults to null if not passed in
        gameObject.Tween(gameObject.name, 0, 1, 0.5f, TweenScaleFunctions.QuadraticEaseIn, updateColor);

    }
    public void FadeOutCanvas()
    {
        Debug.Log(gameObject.name);
        System.Action<ITween<float>> updateColor = (t) =>
        {
            gameObject.GetComponent<CanvasGroup>().alpha = t.CurrentValue;
        };

        System.Action<ITween<float>> ColorCompleted = (t) =>
        {
            gameObject.SetActive(false);
        };
        CanvasGroup g = gameObject.GetComponent<CanvasGroup>();
        g.alpha = 0;

        // completion defaults to null if not passed in

        gameObject.Tween(gameObject.name, 1, 0, 0.5f, TweenScaleFunctions.QuadraticEaseIn, updateColor, ColorCompleted);

    }
}
