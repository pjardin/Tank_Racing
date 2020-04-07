using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonalController : MonoBehaviour
{
    public float jumpHeight;
	public float speed;
	public LayerMask ground;
	public Transform feet; 
	private Vector3 direction;
	private Rigidbody rbody;
	private float rotationSpeed = 2f;
	private float rotationY = 0f;
	private float rotationX = 0;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        jumpHeight = 5.0f;
        speed = 4.0f;
        rbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;

        //turning
        if(direction.x != 0) {
            rotationX += direction.x * rotationSpeed;
            //rbody.MovePosition(rbody.position + transform.right*direction.x*speed*Time.deltaTime);
        }
        //forwards and back
        if(direction.z != 0) {;
        	rbody.MovePosition(rbody.position+transform.forward*direction.z*speed*Time.deltaTime);
        }
        
        
        transform.localEulerAngles = new Vector3(-rotationY,rotationX,0);

        if(Input.GetButtonDown("Fire1")) {
            Debug.Log("fire");
            //audio.Play();
            Fire();
        }
    }
    void Fire() {
    	var bullet = (GameObject)Instantiate(
    		bulletPrefab,
    		bulletSpawn.position,
    		bulletSpawn.rotation);
    	bullet.GetComponent<Rigidbody>().velocity=bullet.transform.forward*50;
    	Destroy(bullet,2.0f);
    }    
}







