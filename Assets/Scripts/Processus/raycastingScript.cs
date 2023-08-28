using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastingScript : MonoBehaviour
{

  /*RAYCAST*/
  [HideInInspector]
  public GameObject lastHit = null;
  [HideInInspector]
  public Vector3 collision = Vector3.zero;
  public GameObject selectedObject = null;

  /*AUDIO*/
  public AudioSource source = null;
  [HideInInspector]
  public string NameGameObject;


    void Update()
    {
      RaycastHit hit;
      Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit, 100)) {
            Debug.Log ("Raycast : "+hit.transform.name);
            Transform tflastHit = hit.transform;
            lastHit = tflastHit.gameObject;
            collision = hit.point;

            SelectObject(lastHit);
      }
      else
      {
            ClearSelection();
      }

    }

    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject) {
                NameGameObject = obj.name;
                //Debug.Log("Le nom de l'objet selectioné: " + obj.name);

                return;
            }

            ClearSelection();
        }
        // AUDIO
        selectedObject = obj;
        if(source!=null){
          source.Stop();
        }
        if (selectedObject.GetComponent<AudioSource>())
        {
            source = selectedObject.GetComponent<AudioSource>();
            source.Play();
        }
    }


    void ClearSelection()
    {
        if (selectedObject == null)
            return;
        selectedObject = null;
    }

    void OnDrawGizmos()
    {
        Update();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, collision);
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
