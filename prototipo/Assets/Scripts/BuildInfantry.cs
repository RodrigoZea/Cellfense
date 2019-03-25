using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInfantry : MonoBehaviour
{
    float currBuild;
    bool cellWorking = false;
    public ParticleSystem psystem;
    public GameObject warrior;
    int workers = 0;
    bool built = false;
    //BloodCellGeneratorScript bcgs;
    // Start is called before the first frame update
    void Start()
    {
        //bcgs = GetComponent<BloodCellGeneratorScript>();
        currBuild = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cellWorking)
        {
            currBuild += 13f * Time.deltaTime * workers;
            Debug.Log(currBuild);
            if (currBuild >= 100) {
                endBuild();
            }
        }
    }

    void instantiateWarrior() {
        Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
        Instantiate(warrior, spawnPos, Quaternion.identity);
    } 


    void endBuild() {
        psystem.Play();
        instantiateWarrior();
        //ResourceManager.Instance.currWorkers = 0;
        ResourceManager.Instance.worked = false;
        built = true;
        // bcgs.enabled = true;
        this.enabled = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            cellWorking = true;
            workers += 1;
        }
    }
}
