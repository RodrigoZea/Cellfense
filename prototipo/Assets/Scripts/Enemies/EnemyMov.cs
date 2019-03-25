using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    private int indexInList;
    private float health = 70;
    NavMeshAgent nva;
    // Start is called before the first frame update
    void Start()
    {
        nva = GetComponent<NavMeshAgent>();
        ResourceManager.Instance.enemySpawns.Add(nva);
        indexInList = ResourceManager.Instance.esIndex;
        ResourceManager.Instance.esIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        findClosest();
    }
        
    void findClosest() {
        NavMeshAgent nearestObj = new NavMeshAgent();
        foreach (var player in ResourceManager.Instance.playerSpawns) {
            nearestObj = ResourceManager.Instance.playerSpawns.FindClosest(player.transform.position);
        }
        if (nearestObj != null) {
            nva.destination = nearestObj.transform.position;
            Debug.DrawLine(gameObject.transform.position, nearestObj.transform.position, Color.blue);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            health -= 35;
            if (health <= 0)
            {
                destroyEnemy();
            }
        }
    }

    void destroyEnemy()
    {
        ResourceManager.Instance.enemySpawns.RemoveAt(indexInList);
        ResourceManager.Instance.esIndex -= 1;
        Destroy(gameObject);
    }
}
