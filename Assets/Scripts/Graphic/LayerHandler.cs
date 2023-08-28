using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerHandler : MonoBehaviour
{
    List<MeshRenderer> meshes;
    // Start is called before the first frame update
    void Start()
    {
        meshes = ConfigScene.Instance.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetLayerAll()
    {
        foreach (MeshRenderer wall in meshes)
        {
                wall.gameObject.layer = LayerMask.NameToLayer("Ange");
        }
    }

    public void changeLayerAll()
    {
        foreach (MeshRenderer wall in meshes)
        {
                wall.gameObject.layer = LayerMask.NameToLayer("Ignore");
        }
    }

    public void changeLayerSingle(MeshRenderer r)
    {
        changeLayerAll();
        if (r.gameObject.layer == LayerMask.NameToLayer("Ignore"))
        {
            r.gameObject.layer = LayerMask.NameToLayer("Ange");
        }
    }

    public void addLayerSingle(MeshRenderer r)
    {
        if (r.gameObject.layer == LayerMask.NameToLayer("Ignore"))
        {
            r.gameObject.layer = LayerMask.NameToLayer("Ange");
        }
    }

}
