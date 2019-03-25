using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBCG : MonoBehaviour
{
    float currBuild;
    bool cellWorking = false;
    bool built = false;
    public GameObject bloodCell;
    public ParticleSystem psystem;
    BloodCellGeneratorScript bcgs;
    int workers = 0;
    // Start is called before the first frame update
    void Start()
    {
        bcgs = GetComponent<BloodCellGeneratorScript>();
        currBuild = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cellWorking)
        {
            Debug.Log(currBuild.ToString());
            currBuild += 15f * Time.deltaTime * workers;
            if (currBuild >= 100)
            {
                endBuild();
            }
        }
    }

    void instantiateBC()
    {
        Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
        Instantiate(bloodCell, spawnPos, Quaternion.identity);
    }


    void endBuild() {
        psystem.Play();
        //ResourceManager.Instance.currWorkers = 0;
        instantiateBC();
        ResourceManager.Instance.worked = false;
        built = true;
        bcgs.enabled = true;
        this.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            cellWorking = true;
            workers += 1;
            //ResourceManager.Instance.currWorkers += 1;
        }
    }
}
