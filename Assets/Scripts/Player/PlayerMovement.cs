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

  public float velocidad = 6.0f;
  public float timeBetweenBullets = 0.15f;
  public GameObject bulletPrefab;
  public Transform bulletSpawn;

  float timer;
  Vector3 movimiento;
  //Animator anim;
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
    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        Debug.Log(bullet.transform.rotation);
    // Add velocity to the bullet
    bullet.GetComponent<Rigidbody>().velocity = -bullet.transform.right * 15;
    // Destroy the bullet after 2 seconds
    Destroy(bullet, 10.0f);
}

void Awake(){
  Piso = LayerMask.GetMask ("Floor");
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

    if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
    {
        Fire ();
    }
  }

}
