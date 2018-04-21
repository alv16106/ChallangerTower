﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaControl : MonoBehaviour {
	public int rebotes = 1;
	// Use this for initialization

	Rigidbody flechaRB;

	void Awake(){
		//anim = GetComponent<Animator>();
		flechaRB = GetComponent<Rigidbody>();
	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(col.gameObject.tag);
    // if (col.gameObject.tag == "Player"){
		//
    // }
		if (col.gameObject.tag == "Terreno") {
			if (!(rebotes>0)) {
				transform.SetParent(col.gameObject.transform, true);
				flechaRB.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
			}
			rebotes--;
		}
	}
}