using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    public GameObject shellExplode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        GameObject explosion = Instantiate(
            shellExplode,
            transform.position,
            transform.rotation);
        Destroy(explosion, 1.0f);
        Destroy(gameObject);
    }
}
