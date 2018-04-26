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

public class PlayerMovement : MonoBehaviour
{
  //Weapon Models
  public GameObject Arco;
  public GameObject Espada;
  public GameObject Escudo;
    public GameObject pelvis;
    public GameObject right;
    public GameObject left;
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


  void Mover(float h, float v){
    movimiento.Set(h, 0f, v);
    movimiento = movimiento.normalized * Time.deltaTime * velocidad;
    //jugadorRB.MovePosition(transform.position + movimiento);
    jugadorRB.MovePosition (transform.position + (transform.forward * movimiento.z) + (transform.right * movimiento.x));
  }

  void Vuelta(){
    Ray camRay = camara.ScreenPointToRay(Input.mousePosition);
    RaycastHit floorhit;
    if (Physics.Raycast(camRay, out floorhit, camRayLargo, Piso)) {
      Vector3 jugadorToMouse = floorhit.point - transform.position;
      jugadorToMouse.y = 0f;
      Quaternion rot = Quaternion.LookRotation(jugadorToMouse);
      jugadorRB.MoveRotation(rot);
    }
  }

  void Fire(){
    timer = 0f;
    // Create the Bullet from the Bullet Prefab
    var bullet = (GameObject)Instantiate(
        bulletPrefab,
        bulletSpawn.position,
        bulletSpawn.rotation);
    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.right * 15;
    // Destroy the bullet after 2 seconds
    Destroy(bullet, 10.0f);
}

void Awake(){
  ph = GetComponent<PlayerHealth>();
  Piso = LayerMask.GetMask ("Floor");
  anima = GetComponent<Animator>();
  //anim = GetComponent<Animator>();
  jugadorRB = GetComponent<Rigidbody>();
}

  void FixedUpdate(){
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");
    Mover(h, v);
    Vuelta();
  }

  void Update(){
    timer += Time.deltaTime;
    timer2 += Time.deltaTime;
    if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && isArco)
    {
      anima.SetTrigger("Bow");
    }else if (Input.GetButton ("Fire1") && !isArco) {
      anima.SetTrigger("Mouse");
    }else if (Input.GetButton ("Shield") && !isArco && timer2 >= escudoCD){
      anima.SetTrigger("Block");
      ph.invulnerable = true;
    }else if (Input.GetButtonDown("Switch")){
      isArco = !isArco;
            if (!isArco)
            {
                Arco.transform.parent = pelvis.transform;
                Arco.transform.localPosition = new Vector3(-0.44f, -0.22f, 0f);
                Arco.transform.localRotation = Quaternion.Euler(90, 90, 0);
                //Poner espada en mano derecha
                Espada.transform.parent = right.transform;
                Espada.transform.localPosition = new Vector3(-0.2f, 0f, 1.54f);
                Espada.transform.localRotation = Quaternion.Euler(0, 90, 90);
                //POner escudo en mano izquierda
                Escudo.transform.parent = left.transform;
                Escudo.transform.localPosition = new Vector3(-0.26f, 0f, 0f);
                Escudo.transform.localRotation = Quaternion.Euler(28, 243, 126);
            }
            if (isArco)
            {
                Arco.transform.parent = left.transform.parent;
                Arco.transform.localPosition = new Vector3(-0.15f, 0.1f, 0f);
                Arco.transform.localRotation = Quaternion.Euler(180, 0, 0);
                Espada.transform.parent = pelvis.transform;
                Espada.transform.localPosition = new Vector3(0.28f, -0.23f, 0f);
                Espada.transform.localRotation = Quaternion.Euler(-90, 90, 180);
                Escudo.transform.parent = pelvis.transform;
                Escudo.transform.localPosition = new Vector3(-0.45f, -0.3f, 0f);
                Escudo.transform.localRotation = Quaternion.Euler(90, 270, 0);
            }
        }
  }

  //Placeholder functions for Animation events
  public void Hit(){
  }

  public void Shoot(){
    Fire();
  }

  public void FootR(){
  }

  public void FootL(){
  }

}
