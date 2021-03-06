﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public bool invulnerable;
    //public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    //Animator anim;
    //AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    //animator
    public Animator anima;


    void Awake ()
    {
        //anim = GetComponent <Animator> ();
        //playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        anima = GetComponent<Animator>();
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

    }


    public void TakeDamage (int amount)
    {
      if (!invulnerable) {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;
      }
        //playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        //playerShooting.DisableEffects ();

        playerMovement.enabled = false;
        //playerShooting.enabled = false;

        //death animation
        anima.SetTrigger("Death");

    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
