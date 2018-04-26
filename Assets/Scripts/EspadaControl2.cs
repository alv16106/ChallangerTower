using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaControl2 : MonoBehaviour {
		public int attackDamage = 15;
		GameObject player;
		PlayerHealth playerHealth;

	// Use this for initialization
	void Awake(){
		Debug.Log("Esta espada es hija de " + transform.parent.tag);
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {
		}

	void OnCollisionEnter(Collision col){
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Player"){
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
