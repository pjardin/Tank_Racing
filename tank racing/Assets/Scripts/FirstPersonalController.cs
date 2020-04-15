using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonalController : MonoBehaviour
{
    //public float jumpHeight;

    private Rigidbody rbody;    //tanks rigid body
    //public LayerMask ground;

    public float CurSpeed;  //current speed of tank in some units
    public float SpeedLimit = 150; //max speed of tank in some units
    public float MaxLimit = 150;    //maximum speed limit
    public float MinLimit = 40; //minimum speed limit
    //public GameObject explosionPrefab;

    //Body/Tread Stuff
    private Vector3 direction;  //tank move input storage
    public Transform TreadL;    //tank tread gameobject containging WheelColliders
    public Transform TreadR;    //tank tread gameobject containging WheelColliders
    public float RotationSpeed = .75f;   //applies multiplier to track speed in opposite directions (can be any number, recomend between 0 and 1, negative inverts controls)
    public float ReverseSpeed = .66f;   //applies multiplier to track speed in when in reverse (can be any number, recomend between 0 and 1, negative means tank cant reverse)
    public float Accel = 1000f; //tank acceleration, value is the torque applied to the tread contact wheel in newtons, the tanks has a lot of mass (negative inverts controls)
    //public float brakespeed = 1000f; //tank breaks, value is the torque applied to the tread contact wheel in newtons, when not accelerating, recommend same as accel at the moment

    //Turret Stuff
    private Vector3 Turretdirection;    //turret rotation input storage
    public GameObject Turret;   //turret body object
    public GameObject Barrel;   //turret barrel object
    private float trotationY = 0f;  //y rotation of turret
    private float trotationX = 0f;  //x rotation of turret
    public float turretRot = 1.0f;  //turret rotation speed, 
    private float turRotLimX = 100f;    //turret left/right turn limit
    private float turRotLimYup = 32f;   //turret up limit in degrees
    private float turRotLimYdn = -11f;  //turret down limit in degrees

    //Bullet Stuff
    public Transform bulletSpawn;   //shell spawn location
    public GameObject bulletPrefab; //shell object to be launched
    private float bulletSpeed = 100;    //speed shells are thrown

    //Audio Stuff
    //private AudioSource Engineaudio;  //self explanitory
    //private AudioSource Fireaudio;    //audio for firing a shell
    //private AudioSource Explosionaudio; //audio for shell exploding


    // Start is called before the first frame update
    void Start()
    {
        //jumpHeight = 5.0f; // tanks cant jump, but...
        rbody = GetComponent<Rigidbody>();
        //audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Vertical"); //GetAxisRaw  is always -1, 0, or 1 so no input lag
        direction.z = Input.GetAxisRaw("Horizontal");
        Turretdirection = Vector3.zero;
        Turretdirection.x = Input.GetAxis("Horizontal2");
        Turretdirection.y = Input.GetAxis("Vertical2");

        //Forward/Rotational movement
        if (direction.x < 0) { direction.x = direction.x * ReverseSpeed; } // if reversing apply reverse speed mutiplier
        CurSpeed = rbody.velocity.magnitude * 3.6f;

        float AccelR = Accel;
        if (direction.x != 0) { AccelR = AccelR * (direction.z); }  //apply direction to acceleration
        if (direction.z != 0) { AccelR = AccelR * (-RotationSpeed * direction.z); } // if turnig apply rotation speed mutiplier
        if (CurSpeed > SpeedLimit && direction.z == 0) { AccelR = 0; } //if above speed limit and not turing, motors apply 0 torque

        float AccelL = Accel;
        if (direction.x != 0) { AccelL = AccelL * (direction.z); }
        if (direction.z != 0) { AccelL = AccelL * (RotationSpeed * direction.z); } // if turnig apply rotation speed mutiplier opposite of right tread
        if (CurSpeed > SpeedLimit && direction.z == 0) { AccelL = 0; }
        
        foreach (Transform contact in TreadR) { //for each WheelCollider in TreadR apply torque in direction to tank, this is really janky, need to adjust traction on wheelcollision objects
            WheelCollider w = contact.GetComponent<WheelCollider>();
            if (direction != Vector3.zero) {
                w.brakeTorque = 0;
                w.motorTorque = AccelR;
            } else {
                w.brakeTorque = Accel;
                w.motorTorque = 0;
            }
        }

        foreach (Transform contact in TreadL) {
            WheelCollider w = contact.GetComponent<WheelCollider>();
            if (direction != Vector3.zero) {
                w.brakeTorque = 0;
                w.motorTorque = AccelL;
            } else {
                w.brakeTorque = Accel;
                w.motorTorque = 0;
            }  
        }

        //Turret rotation
        if (Turretdirection.x != 0 || Turretdirection.y != 0) {
            trotationX += Turretdirection.x * turretRot;
            trotationY += Turretdirection.y * turretRot;
            
            //limit x turret rotation
            if (trotationX > turRotLimX) {
                trotationX = turRotLimX;
            } else if (trotationX < -turRotLimX) {
                trotationX = -turRotLimX; 
            }

            //limit y turret rotation
            if (trotationY > -turRotLimYdn) {
                trotationY = -turRotLimYdn;
            } else if (trotationY < -turRotLimYup) {
                trotationY = -turRotLimYup;
            }
        }

        Turret.transform.localEulerAngles = new Vector3(0, trotationX, 0);  //apply rotation to turret body
        Barrel.transform.localEulerAngles = new Vector3(trotationY, 0, 0);  //apply rotation to turret barrel

        //Fire!!!
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("fire");
            //audio.Play();
            Fire();
        }

    }

    void OnCollisionEnter(Collision collision )
    {
        if (collision.gameObject.tag == "Russian Water") {
            if (SpeedLimit < MaxLimit) {
                SpeedLimit = SpeedLimit + 10; 
            }
            Destroy(collision.gameObject); 
        }
        if (collision.gameObject.tag == "Regular Water")
        {
            if (SpeedLimit > MinLimit)
            {
                SpeedLimit = SpeedLimit - 10;
            }
            Destroy(collision.gameObject);
        }
    }

    void Fire() {
    	var bullet = (GameObject)Instantiate(
    		bulletPrefab,
    		bulletSpawn.position,
    		bulletSpawn.rotation);
    	bullet.GetComponent<Rigidbody>().velocity=bullet.transform.up * bulletSpeed;
    	Destroy(bullet,5.0f);
    }
}
