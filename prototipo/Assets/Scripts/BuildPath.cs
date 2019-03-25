using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPath : MonoBehaviour
{
    float currBuild;
    bool cellWorking = false;
    bool built = false;
    public ParticleSystem psystem;
    int workers = 0;
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
            currBuild += 20f * Time.deltaTime * workers;
            if (currBuild >= 100)
            {
                endBuild();
            }
        }
    }

    void endBuild()
    {
        psystem.Play();
        //ResourceManager.Instance.currWorkers = 0;
        ResourceManager.Instance.worked = false;
        built = true;
        //ResourceManager.Instance.currPosition = 
        // bcgs.enabled = true;
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
