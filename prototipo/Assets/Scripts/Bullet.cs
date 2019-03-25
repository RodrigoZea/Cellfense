using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 35f;
    public GameObject impactEffect;
    public AudioClip laser;
    private AudioSource bas;

    public void Seek(Transform _target){
        target = _target;
    }
    // Start is called before the first frame update
    void Start()
    {
        bas = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget() {
        GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        bas.PlayOneShot(laser, 0.20f);
        Destroy(effectIns, 2f);
        Destroy(gameObject);
    }
}
