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
    public GameObject bloodCell;
    //public GameObject baker;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = new GameObject();
        Instantiate(bloodCell, new Vector3(0, 1.19f, 0), Quaternion.identity);
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


    public void spawnSomething(int limit, GameObject cube) {
        spawnLog();

        if (ResourceManager.Instance.bloodPts >= limit)
        {
            ResourceManager.Instance.deductPoints(limit);

            GameObject newCube = (GameObject)Instantiate(cube, go.transform.position, Quaternion.identity);
            NavMeshSurface nms = newCube.GetComponent<NavMeshSurface>();

            addToBake(nms);
            ResourceManager.Instance.positionList.Add(newCube.transform.position);
            ResourceManager.Instance.updatePos(newCube.transform.position);
        }

        destroyLog();
    }

    public void spawnBCG() {
        spawnSomething(10, bcg);
    }

    public void spawnI() {
        spawnSomething(20, infantry);
    }

    public void spawnPath()
    {
        spawnSomething(5, path);
    }

}
