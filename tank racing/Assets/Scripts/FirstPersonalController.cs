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
	private float rotationSpeed = 1.0f;//.15f; //tanks aren't known for being able to turn on a dime
    private float turretRot = 1.0f;//.15f; //tanks aren't known for being able to turn on a dime
    private float rotationY = 0f;
	private float rotationX = 0;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	//private AudioSource audio;

    private Vector3 Turretdirection;
    public GameObject Turret;
    public GameObject Barrel;
    private float trotationY = 0f;
    private float trotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        jumpHeight = 5.0f; // tanks cant jump, but...
        speed = 4.0f;
        rbody = GetComponent<Rigidbody>();
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        Turretdirection = Vector3.zero;
        Turretdirection.x = Input.GetAxis("Horizontal2");
        Turretdirection.y = Input.GetAxis("Vertical2");
        //  direction = direction.normalized; //makes output 1 or 0 instead of a gradient

        //turning
        if (direction.x != 0) {
            rotationX += direction.x * rotationSpeed;
            //rbody.MovePosition(rbody.position + transform.right*direction.x*speed*Time.deltaTime);
        }
        //forwards and back
        if(direction.z != 0) {;
        	rbody.MovePosition(rbody.position+transform.forward*direction.z*speed*Time.deltaTime);
        }

        //Turret movement
        if (Turretdirection.x != 0)
        {
            trotationX += Turretdirection.x * (rotationSpeed * 2);
            //rbody.MovePosition(rbody.position + transform.right*direction.x*speed*Time.deltaTime);
        }
        if (Turretdirection.y != 0)
        {
            trotationY += Turretdirection.y * (rotationSpeed * 2);
        }


        transform.localEulerAngles = new Vector3(-rotationY,rotationX,0);
        Turret.transform.localEulerAngles = new Vector3(0, trotationX, 0);
        Barrel.transform.localEulerAngles = new Vector3(trotationY, 0, 0);

        if (Input.GetButtonDown("Fire1")) {
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
    	bullet.GetComponent<Rigidbody>().velocity=bullet.transform.up*50;
    	Destroy(bullet,5.0f);
    }    
}







