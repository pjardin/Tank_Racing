using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    public GameObject shellExplode;
    public GameObject BuildingExplode;
    //public GameObject DebrisExplode;

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

        if (collision.gameObject.tag == "Debris") {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Building") {
            GameObject bullet = Instantiate(
                BuildingExplode,
                collision.gameObject.transform.position,
                Quaternion.Euler(new Vector3(-90, 0, 0)));
            Transform shell = GetComponent<Transform>();
            new WaitForSeconds(2.0f);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(shell.up * 17000, shell.transform.position);
            //collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            Destroy(collision.gameObject, 2.0f);
            Destroy(BuildingExplode, 10.0f);
        }
    }
}
