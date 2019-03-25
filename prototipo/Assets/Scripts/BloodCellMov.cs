using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodCellMov : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 startPosition;
    private int indexInList;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.playerSpawns.Add(agent);
        indexInList = ResourceManager.Instance.psIndex;
        ResourceManager.Instance.psIndex++;
        //Debug.Log(ResourceManager.Instance.playerSpawns.Count);
    }

    // Update is called once per frame
    void Update()
    {
        //if (ResourceManager.Instance.worked == true && ResourceManager.Instance.currWorkers == 0)
        if (ResourceManager.Instance.worked == true) {    
            agent.destination = ResourceManager.Instance.currPosition;
        }
        else {
            agent.destination = startPosition;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy") {
            health -= 1;
            if (health <= 0) {
                deleteBloodCell();
            }
        }
        
    }

    private void deleteBloodCell() {
        ResourceManager.Instance.playerSpawns.RemoveAt(indexInList);
        if (ResourceManager.Instance.playerSpawns.Count == 0) {
            ResourceManager.Instance.gameLost();
        }
        Destroy(gameObject);
    }

}
