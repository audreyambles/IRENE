using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlgoBrain : MonoBehaviour
{
    public GameObject[] modeleListe;
    public GameObject modeleActif;
    //pour l'instant il faut mettre les stim à la main
    public StimulusData[] stimulusListe;
    public casUsage cas1;

    public AudioSource source;
    public ShaderHelper shaderModif;
    public LightHandler lightModif;

    public GameObject messageSceneToucher;
    public GameObject messageSceneOdeur;

    //attention, pour le moment on teste et vu qu'on est dans une boucle update, si on ne vérifie pas avec un flag qu'on a déjà joué le son, ça boucle sur le son et c'est horrible
    public bool sonOK = false;
    public bool lightOK = false;


    void Awake()
    {
        modeleListe = GameObject.FindGameObjectsWithTag("Objet");
        

        //Debug.Log("DEBUT DU DEBUG SPECIAL DATA");
        //Debug.Log("Nom usager :" + cas1.usager1.nom);
        //Debug.Log("--------------MODELES----------------");
        //Debug.Log("LES MODELES : ");
        //for (int i = 0; i < modeleListe.Length; i++)
        //{
        //    Debug.Log("Nom du GameObject : " + modeleListe[i].ToString() + " - nom de son Data : " + modeleListe[i].GetComponent<Modele3DController>().modeleData.nom);
        //}
        //Debug.Log("---------------STIM---------------");
        //Debug.Log("TOUS LES STIMS : ");
        //for (int i=0; i<stimulusListe.Length; i++)
        //{
        //    Debug.Log(stimulusListe[i].nom + " : " + stimulusListe[i].attribut + " - " + stimulusListe[i].valeur);
        //}
        //playerAudio = GameObject.Find("Modele3D");
        //source = playerAudio.GetComponent<AudioSource>();

    }

    public void Start()
    {
        messageSceneToucher.SetActive(false);
        messageSceneOdeur.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < modeleListe.Length; i++)
        {
            if (modeleListe[i].activeSelf)
            {
                if(modeleActif != modeleListe[i])
                {
                    modeleActif = modeleListe[i];
                    shaderModif.GetComponent<ShaderHelper>().choix.value = 7;
                    lightModif.GetComponent<LightHandler>().DesactiverDollyLightModel();
                    sonOK = false;
                    lightOK = false;
                    messageSceneToucher.SetActive(false);
                    messageSceneOdeur.SetActive(false);
                }

            }
        }


        //Debug.Log("ON TEST : ... " + modeleActif.name);
        for (int i = 0; i < stimulusListe.Length; i++)
        {
            if (modeleActif.GetComponent<Modele3DController>().modeleData.forme == stimulusListe[i].valeur)
            {
                if (stimulusListe[i] is VueData)
                {
                    declenchementContours((VueData)stimulusListe[i]);
                }
            }

            if (modeleActif.GetComponent<Modele3DController>().modeleData.couleur == stimulusListe[i].valeur)
            {
                if (stimulusListe[i] is VueData && lightOK == false)
                {
                    declenchementTaille((VueData)stimulusListe[i]);
                }
            }

            if (modeleActif.GetComponent<Modele3DController>().modeleData.son == stimulusListe[i].valeur)
            {
                if (stimulusListe[i] is SonData && sonOK == false)
                {
                    declenchementSon((SonData)stimulusListe[i]);
                    //test pour ne pas boucler (cf explications plus haut)
                    
                }
            }

            if (modeleActif.GetComponent<Modele3DController>().modeleData.textureTouch == stimulusListe[i].valeur)
            {
                if (stimulusListe[i] is TouchData)
                {
                    declenchementTexteT((TouchData)stimulusListe[i]);

                }
            }

            if (modeleActif.GetComponent<Modele3DController>().modeleData.signatureOd == stimulusListe[i].valeur)
            {
                if (stimulusListe[i] is OdeurData)
                {
                    declenchementTexteO((OdeurData)stimulusListe[i]);

                }
            }
        }
    }

    public void declenchementTexteT(TouchData stimTouch)
    {
        messageSceneToucher.SetActive(true);
        messageSceneToucher.GetComponent<TMP_Text>().text = stimTouch.toucher;
    }

    public void declenchementTexteO(OdeurData stimOdeur)
    {
        messageSceneOdeur.SetActive(true);
        messageSceneOdeur.GetComponent<TMP_Text>().text = stimOdeur.odeur;
    }

    public void declenchementContours(VueData instrucVue)
    {
        //pour les VueData (=stimuli vue) il faudrait dans le type VueData mettre ces paramètres (contours.choix.value = le type de contours, colorshader=la couleur) etc)
        //comme ça on a pas besoin de faire instruction1, instruction2 ... (à modifier si on part sur ce type d'héritage stimuli => vue / ouie / touch...)
        if(instrucVue.instruction == "contours1")
        {
            shaderModif.GetComponent<ShaderHelper>().choix.value = 1;
            shaderModif.GetComponent<ShaderHelper>().modifColorShader3();
            shaderModif.GetComponent<ShaderHelper>().modifBorderShader(0.0008f);
            shaderModif.GetComponent<ShaderHelper>().changeMaterialContour();
        }
        if (instrucVue.instruction == "contours2")
        {
            shaderModif.GetComponent<ShaderHelper>().choix.value = 2;
            shaderModif.GetComponent<ShaderHelper>().modifColorShader6();
            shaderModif.GetComponent<ShaderHelper>().modifBorderShader(0.0008f);
            shaderModif.GetComponent<ShaderHelper>().changeMaterialContour();
        }
        if (instrucVue.instruction == "contours3")
        {
            shaderModif.GetComponent<ShaderHelper>().choix.value = 4;
            shaderModif.GetComponent<ShaderHelper>().modifColorShader7();
            shaderModif.GetComponent<ShaderHelper>().modifBorderShader(0.001f);
            shaderModif.GetComponent<ShaderHelper>().changeMaterialContour();
        }
    }

    public void declenchementTaille(VueData instrucVue)
    {
        if (instrucVue.instruction == "grandeur")
        {
            lightModif.GetComponent<LightHandler>().SwitchDollyLightModelOnly(1);
            lightModif.GetComponent<LightHandler>().speedDollyLightModel(50);
            lightOK = true;
        }
    }

    public void declenchementSon(SonData decSon)
    {
        source.PlayOneShot(decSon.bandeSon, 1);
        sonOK = true;
    }


}
