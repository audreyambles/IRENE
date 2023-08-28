using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChange : MonoBehaviour
{
    List<MeshRenderer> meshesInScene;
    public List<Mesh> remplacementAnge;
    public void Start()
    {
        meshesInScene = ConfigScene.Instance.mesh;

    }
    public void ModelChangeMesh()
    {
        int i = 0;
        foreach (MeshRenderer m in ConfigScene.Instance.mesh)
        {
            m.gameObject.GetComponent<MeshFilter>().mesh = remplacementAnge[i];
            i++;
        }
    }

    public void resetModel()
    {
        ConfigScene.Instance.mesh = meshesInScene;
    }
}
