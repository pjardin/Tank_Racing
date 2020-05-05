using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonalController : MonoBehaviour
{
    //public float jumpHeight;

    private Rigidbody rbody;    //tanks rigid body
                                //public LayerMask ground;
    public bool mouseEnable = false;
    

    //Movement
    public float curSpeed;  //current speed of tank in some units
    public float speedLimit = 40; //max speed of tank in some units
    public float maxLimit = 150;    //maximum speed limit
    public float minLimit = 30; //minimum speed limit
    public float vodkaSpeed = 10;
    public float rotationSpeed = .75f;   //applies multiplier to track speed in opposite directions (can be any number, recomend between 0 and 1, negative inverts controls)
    public float reverseSpeed = .66f;   //applies multiplier to track speed in when in reverse (can be any number, recomend between 0 and 1, negative means tank cant reverse)
    public float accel = 1000f; //tank acceleration, value is the torque applied to the tread contact wheel in newtons, the tanks has a lot of mass (negative inverts controls)
    

    //Camera
    public GameObject camPivot;
    public float camRot = 500;

    //Body/Tread Stuff
    private Vector3 direction;  //tank move input storage
    public Transform treadL;    //tank tread gameobject containging WheelColliders
    public Transform treadR;    //tank tread gameobject containging WheelColliders

    //Turret Stuff
    private Vector3 turretDirection;    //turret rotation input storage
    public GameObject turret;   //turret body object
    public GameObject barrel;   //turret barrel object
    private float trotationY = 0f;  //y rotation of turret
    private float trotationX = 0f;  //x rotation of turret
    public float turretRot = 1.0f;  //turret rotation speed, 
    private float turRotLimX = 100f;    //turret left/right turn limit
    private float turRotLimYUp = 32f;   //turret up limit in degrees
    private float turRotLimYDn = -11f;  //turret down limit in degrees
    

    //Bullet Stuff
    public Transform bulletSpawn;   //shell spawn location
    public GameObject bulletPrefab; //shell object to be launched
    private float bulletSpeed = 100;    //speed shells are thrown
    public float recoil = 250000;
    private float cooldown = 0;
    public float fireRate = 1;
    public int maxAmmo = 50;
    public int ammo = 50;

    //Audio Stuff
    private AudioSource[] aSource; //array of all AudioSource Components
    private AudioSource fireSFX; //sound that playes when firing
    private AudioSource engineSFX;   //Sound that playes when driving
    private bool curDriv;    //checks if tank engine is currently running

    //Special Effects
    private bool Ldust = false;
    public ParticleSystem dustSpawnLr;
    public ParticleSystem dustSpawnLf;
    private bool Rdust = false;
    public ParticleSystem dustSpawnRr;
    public ParticleSystem dustSpawnRf;
    public ParticleSystem muzzle;
    public GameObject compass;
    public Transform North;

    // Start is called before the first frame update
    void Start() {
        //jumpHeight = 5.0f; // tanks cant jump, but...
        rbody = GetComponent<Rigidbody>();

        aSource = GetComponents<AudioSource>();
        fireSFX = aSource[0];
        engineSFX = aSource[1];

        dustSpawnLf.Stop();
        dustSpawnLr.Stop();
        dustSpawnRf.Stop();
        dustSpawnRr.Stop();
        muzzle.Stop();
        curDriv = false;

    }

    // Update is called once per frame
    private void Update() {
        compass.transform.LookAt(North);

        direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Vertical"); //GetAxisRaw  is always -1, 0, or 1 so no input lag
        direction.z = Input.GetAxisRaw("Horizontal");
        turretDirection = Vector3.zero;
        if (mouseEnable) {
            turretDirection.x = Input.GetAxis("Mouse X");
            turretDirection.y = -Input.GetAxis("Mouse Y");
        } else {
            turretDirection.x = Input.GetAxis("Horizontal2");
            turretDirection.y = Input.GetAxis("Vertical2");
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            float f = Mathf.MoveTowardsAngle(camPivot.transform.localEulerAngles.y, trotationX, camRot * Time.deltaTime);
            camPivot.transform.localEulerAngles = new Vector3(0, f, 0);
        } else {

            float f = Mathf.MoveTowardsAngle(camPivot.transform.localEulerAngles.y, 0, camRot * Time.deltaTime);
            camPivot.transform.localEulerAngles = new Vector3(0, f, 0);
        }

        //Forward/Rotational movement
        if (direction.x < 0) { direction.x *= reverseSpeed; } // if reversing apply reverse speed mutiplier
        curSpeed = rbody.velocity.magnitude * 3.6f;
        float accelR = accel;
        if (direction.x != 0) {
            accelR *= (direction.x);    //apply direction to acceleration
            EngineSound();
        }  
        if (direction.z != 0) {
            accelR *= (-rotationSpeed * direction.z);   // if turnig apply rotation speed mutiplier
            EngineSound();
        } 
        if (direction.x == 0 && direction.z == 0)
        {
            curDriv = false;
            engineSFX.Stop();
        }
        if ((direction != Vector3.zero) && (curSpeed >= 3.0f) && !Rdust) {
            Rdust = true;
            dustSpawnRr.Play();
            dustSpawnRf.Play(); 
        } else if (direction == Vector3.zero && curSpeed < 3.0f && Rdust) {
            Rdust = false;
            dustSpawnRr.Stop();
            dustSpawnRf.Stop(); 
        }
        if (curSpeed > speedLimit && direction.z == 0) { accelR = 0; } //if above speed limit and not turing, motors apply 0 torque

        float accelL = accel;
        if (direction.x != 0) { accelL *= (direction.x); }
        if (direction.z != 0) { accelL *= (rotationSpeed * direction.z); } // if turnig apply rotation speed mutiplier opposite of right tread        pS.enableEmission = true;
        if (direction != Vector3.zero && curSpeed >= 3.0f && !Ldust) {
            Ldust = true;
            dustSpawnLr.Play(); 
            dustSpawnLf.Play(); 
        } else if (direction == Vector3.zero && curSpeed < 3.0f && Ldust) {
            Ldust = false;
            dustSpawnLr.Stop(); 
            dustSpawnLf.Stop(); 
        }
        if (curSpeed > speedLimit && direction.z == 0) { accelL = 0; }

        foreach (Transform contact in treadR) { //for each WheelCollider in TreadR apply torque in direction to tank, this is really janky, need to adjust traction on wheelcollision objects
            WheelCollider w = contact.GetComponent<WheelCollider>();
            if (direction != Vector3.zero) {
                w.brakeTorque = 0;
                w.motorTorque = accelR;
            } else {
                w.brakeTorque = accel;
                w.motorTorque = 0;
            }
        }

        foreach (Transform contact in treadL) {
            WheelCollider w = contact.GetComponent<WheelCollider>();
            if (direction != Vector3.zero) {
                w.brakeTorque = 0;
                w.motorTorque = accelL;
            } else {
                w.brakeTorque = accel;
                w.motorTorque = 0;
            }
        }

        //Turret rotation
        if (turretDirection.x != 0 || turretDirection.y != 0) {
            trotationX += turretDirection.x * turretRot;
            trotationY += turretDirection.y * turretRot;
            
            //limit x turret rotation
            if (trotationX > turRotLimX) {
                trotationX = turRotLimX;
            } else if (trotationX < -turRotLimX) {
                trotationX = -turRotLimX;
            }

            //limit y turret rotation
            if (trotationY > -turRotLimYDn) {
                trotationY = -turRotLimYDn;
            } else if (trotationY < -turRotLimYUp) {
                trotationY = -turRotLimYUp;
            }
        }

        turret.transform.localEulerAngles = new Vector3(0, trotationX, 0);  //apply rotation to turret body
        barrel.transform.localEulerAngles = new Vector3(trotationY, 0, 0);  //apply rotation to turret barrel

        //Fire!!!
        if (Input.GetButtonDown("Fire1") && Time.time > cooldown && ammo > 0) {
            fireSFX.Play();
            cooldown = Time.time + fireRate;
            ammo--;
            Fire();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Russian Water")) {
            if (speedLimit < maxLimit) {
                speedLimit += vodkaSpeed;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Regular Water") {
            if (speedLimit > minLimit) {
                speedLimit -= vodkaSpeed;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ammo") {
            ammo += 10;
            if (ammo > maxAmmo) {
                ammo = maxAmmo;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Debris") {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false; 
        }
    }

    private void Fire() {
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;
        muzzle.Play();
        rbody.AddForceAtPosition(-bulletSpawn.up * recoil, bulletSpawn.transform.position);
        Destroy(bullet, 5.0f);
    }

    private void EngineSound()
    {
        if (!curDriv)
        {
            curDriv = true;
            engineSFX.Play();
        }
    }
}
