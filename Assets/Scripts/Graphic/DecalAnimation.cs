using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script pour aider à animer un décal au travers de son shader, marche seulement avec le shader "Decal"
/// L'animation peut apparaitre saccadee, il est preferable de ne pas utiliser ce script et plutot des solutions annexes comme le shader "AnimationDecalFlipbook" ou 
/// une animation via Animator et les lightCookies. 
/// </summary>
public class DecalAnimation : MonoBehaviour
{
    /// <summary>
    /// Animation, listes de texture 2D 
    /// </summary>
    public Sprite textureAdded;
    private Material decalmat;
    private Texture tex;

    // Start is called before the first frame update
    void Start()
    {
        tex = decalmat.mainTexture;

    }

    //Il est impossible d'animer directement avec un sprite, une conversion est donc necessaire.
    public Texture2D SpriteToTexture(Sprite sprite)
    {
        // assume "sprite" is your Sprite object
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }
    // Update is called once per frame
    void Update()
    {
        if(textureAdded != null)
        {
            UpdateTexture(SpriteToTexture(textureAdded));
        }
    }

    public void UpdateTexture(Texture t)
    {
        Debug.Log(decalmat.name);
        decalmat.SetTexture("_BaseColorMap", t);
       
    }
}
