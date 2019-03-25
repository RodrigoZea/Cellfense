using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BloodCellMov : MonoBehaviour
{
    public GameObject impactEffect;
    NavMeshAgent agent;
    Vector3 startPosition;
    private float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.playerList.Add(gameObject);
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
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        ResourceManager.Instance.playerList.Remove(gameObject);
        Destroy(gameObject);
        if (ResourceManager.Instance.playerList.Count == 0) {
            ResourceManager.Instance.gameLost();
        }
    }

}
