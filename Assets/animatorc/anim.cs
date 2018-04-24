using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour {

    Animator anima;

	// Use this for initialization
	void Start () {
        anima = GetComponent<Animator>();

		
	}
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Vertical");
        anima.SetFloat("Vel", move);
        if (Input.GetMouseButtonDown(0))
        {
            anima.SetTrigger("Mouse");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anima.SetTrigger("Block");
        }
        if (Input.GetMouseButtonDown(2))
        {
            anima.SetTrigger("Bow");
        }

    }
}
