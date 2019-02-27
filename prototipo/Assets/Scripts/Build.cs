using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    float currBuild;
    bool cellWorking = false;
    public ParticleSystem psystem;
    // Start is called before the first frame update
    void Start()
    {
        currBuild = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cellWorking)
        {
            currBuild += 5f;
            if (currBuild >= 100) {
                endBuild();
            }
        }
    }

    void endBuild() {
        psystem.Play();
        this.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            cellWorking = true;
        }
    }
}
