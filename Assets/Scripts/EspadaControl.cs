using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaControl : MonoBehaviour {
		public int attackDamage = 15;
		GameObject player;
		PlayerHealth playerHealth;

	// Use this for initialization
	void Awake(){
		Debug.Log("Esta espada es hija de " + transform.parent.tag);
		player = GameObject.FindGameObjectWithTag ("Player2");
		playerHealth = player.GetComponent <PlayerHealth> ();
	}

	// Update is called once per frame
	void Update () {
		}

	void OnCollisionEnter(Collision col){
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Player2"){
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
