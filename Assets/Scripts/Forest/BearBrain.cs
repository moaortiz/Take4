﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBrain : MonoBehaviour
{
    //Wander if it cant see the player
    //If we can see the bear, it will evade
    //If the hive is dropped we will seek hive
    private Bot bot;
    private Vector3 hivePos;
    private bool hiveDropped = false;
    private bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        bot = GetComponent<Bot>();
        NavPlayerMovement.DroppedHive += HiveReady;
    }

    void HiveReady(Vector3 pos)
    {
        hivePos = pos;
        hiveDropped = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isStopped)
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
            if (hiveDropped)
            {
                bot.Seek(hivePos);
            }
            else
            {
                if (bot.CanTargetSeeMe())
                {
                    bot.Evade();
                }
                else if (bot.CanSeeTarget())
                {
                    bot.Pursue();
                }
                else
                {

                    bot.Wander();
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            bot.Stop();
            isStopped = true;
        }
    }
}
