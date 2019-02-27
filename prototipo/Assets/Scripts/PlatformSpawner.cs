using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformSpawner : MonoBehaviour
{
    GameObject texts;
    GameObject[] options;
    //NavMeshBaker nvb;
    public GameObject bcg;
    public GameObject infantry;
    public GameObject path;
    //public GameObject baker;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnLog() {
        options = GameObject.FindGameObjectsWithTag("Creators");

        foreach (GameObject creator in options)
        {
            if (creator.transform.childCount > 0)
            {
                go = creator;
                break;
            }
        }
    }

    void destroyLog() {
        texts = GameObject.FindGameObjectWithTag("Options");
        Destroy(texts);
    }

    void addToBake(NavMeshSurface nms) {
        NavMeshBaker.Instance.navMeshSurfaces.Add(nms);
        NavMeshBaker.Instance.Bake();
    }


    public void spawnBCG() {
        spawnLog();

        GameObject newBCG = (GameObject) Instantiate(bcg, go.transform.position, Quaternion.identity);
        NavMeshSurface nms = newBCG.GetComponent<NavMeshSurface>();

        addToBake(nms);

        destroyLog();
    }

    public void spawnI() {
        spawnLog();

        GameObject newInfantry = (GameObject) Instantiate(infantry, go.transform.position, Quaternion.identity);
        NavMeshSurface nms = newInfantry.GetComponent<NavMeshSurface>();

        addToBake(nms);

        destroyLog();
    }

    public void spawnPath()
    {
        spawnLog();

        GameObject newPath = (GameObject) Instantiate(path, go.transform.position, Quaternion.identity);
        NavMeshSurface nms = newPath.GetComponent<NavMeshSurface>();

        addToBake(nms);

        destroyLog();
    }

}
