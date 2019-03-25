using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        hasCollided = false;  
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Infantry" || collision.collider.gameObject.tag == "BloodFactory" || collision.collider.gameObject.tag == "Mesh")
        {
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies() {
        if (hasCollided == false) {
            for (int i = 0; i < ResourceManager.Instance.roundLimit; i++)
            {
                Vector3 enemyPos = new Vector3(this.gameObject.transform.position.x - 2, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

                Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
                yield return new WaitForSeconds(2f);
            }
            hasCollided = true;
        }
        Destroy(this.gameObject);

    }
}
