using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCellGeneratorScript : MonoBehaviour
{
    float timer = 0;
    public int bloodGain = 2;
    public ParticleSystem psystem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f) {
            ResourceManager.Instance.addPts(bloodGain);
            timer = 0;
            psystem.Play();
        }
    }
}
