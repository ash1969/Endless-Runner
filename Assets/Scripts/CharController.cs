﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public Transform rayStart;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if(!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("gameStarted");
        }
        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        RaycastHit hit;
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            anim.SetTrigger("isFalling");
        }

        if(transform.position.y < -2)
        {
            gameManager.EndGame();
        }
    }

    private void Switch()
    {
        walkingRight = !walkingRight;

        if (walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }
}
