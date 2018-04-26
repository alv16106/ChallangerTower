using UnityEngine;

// public enum Weapon{
// 	UNARMED = 0,
// 	TWOHANDBOW = 1,
// 	ARMEDSHIELD = 2
// }
//
// public enum RPGCharacterState{
// 	DEFAULT,
// 	BLOCKING,
// 	STRAFING,
// 	CLIMBING,
// 	SWIMMING
// }

public class PlayerMovement2 : MonoBehaviour
{
    //Weapon Models
    public GameObject Arco;
    public GameObject Espada;
    public GameObject Escudo;
    public Camera camara;
    public bool isArco;
    public bool tieneEscudo;

    public float velocidad = 6.0f;
    public float timeBetweenBullets = 0.15f;
    public float escudoCD = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    PlayerHealth ph;
    float timer;
    float timer2;
    Vector3 movimiento;
    Animator anima;
    Rigidbody jugadorRB;
    int Piso;
    float camRayLargo = 100f;


    void Mover(float h, float v)
    {
        movimiento.Set(h, 0f, v);
        movimiento = movimiento.normalized * Time.deltaTime * velocidad;
        //jugadorRB.MovePosition(transform.position + movimiento);
        jugadorRB.MovePosition(transform.position + (transform.forward * movimiento.z) + (transform.right * movimiento.x));
    }

    void Vuelta()
    {
        var angH = Input.GetAxis("Rotatex");
        var angW = Input.GetAxis("Rotatey");
        if(angH!=0 && angW != 0)
        {
            var pls = new Vector3(angH, 0, angW);
            Quaternion rot = Quaternion.LookRotation(pls);
            jugadorRB.MoveRotation(rot);
        }
        
    }

    void Fire()
    {
        timer = 0f;
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
        Debug.Log(bullet.transform.rotation);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.right * 15;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 10.0f);
    }

    void Awake()
    {
        ph = GetComponent<PlayerHealth>();
        Piso = LayerMask.GetMask("Floor");
        anima = GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        jugadorRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal2");
        float v = Input.GetAxisRaw("Vertical2");
        Mover(h, v);
        Vuelta();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (Input.GetButton("Fire1C") && timer >= timeBetweenBullets && Time.timeScale != 0 && isArco)
        {
            anima.SetTrigger("Bow");
        }
        else if (Input.GetButton("Fire1C") && !isArco)
        {
            //anima.SetTrigger("Mouse");
        }
        else if (Input.GetButton("ShieldC") && !isArco && timer2 >= escudoCD)
        {
            anima.SetTrigger("Block");
            ph.invulnerable = true;
        }
        else if (Input.GetMouseButtonDown(2))
        {
            isArco = !isArco;
        }
    }

    //Placeholder functions for Animation events
    public void Hit()
    {
    }

    public void Shoot()
    {
        Fire();
    }

    public void FootR()
    {
    }

    public void FootL()
    {
    }

}
