using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectClicker : MonoBehaviour{
    public GameObject floatingText;
    public GameObject deleteText;
    GameObject[] texts, deletetexts;
    //public UnityEngine.Events.UnityEvent displayOptions;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "Creators")
                    {
                        OnCreatorClick(hit.transform.gameObject, "Options", texts, floatingText);
                    }

                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "Mesh" || hit.transform.gameObject.tag == "Infantry" || hit.transform.gameObject.tag == "BloodFactory")
                    {
                        deleteSomething(hit.transform.gameObject);
                    }
                }

            }
        }
    }

    void deleteSomething(GameObject go) {
        Destroy(go);
        NavMeshBaker.Instance.navMeshSurfaces.Remove(go.GetComponent<NavMeshSurface>());
        NavMeshBaker.Instance.Bake();
        addPoints(go.tag);
    }

    void addPoints(string tag) {
            switch (tag) { 
                case "Mesh":
                    ResourceManager.Instance.addPts(3);
                    break;
                case "Infantry":
                    ResourceManager.Instance.addPts(10);
                    break;
                case "BloodFactory":
                    ResourceManager.Instance.addPts(5);
                    break;
            }
    }

    void OnCreatorClick(GameObject go, string tag, GameObject[] texts, GameObject floatingText) {
        Vector3 textPosition = new Vector3(go.transform.position.x + 3, go.transform.position.y + 5, go.transform.position.z - 2.3f);
        texts = GameObject.FindGameObjectsWithTag(tag);

        if (texts.Length > 0) {
            foreach (GameObject text in texts)
            {
                Destroy(text);
            }
        }
        
        if (floatingText) {
            Instantiate(floatingText, textPosition, go.transform.rotation * Quaternion.Euler(30f, 45f, 0f), go.transform);
        }
    }

}
