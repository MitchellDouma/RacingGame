using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {

    const float SPEED = 5000f;
    const float TURNSPEED = 8000f;
    private float powerInput;
    public float turnInput;
    bool brake;
    private Rigidbody carRigidbody;

    bool onRoad = true;
    bool inMiddle;
    bool isTurning;

    AIPath ai;

    public int lapCounter;
    public int carNumber;
    public int place;
    public int checkPointCounter;

    public AudioSource soundSource;
    public AudioSource engineSource;
    public AudioClip[] crashSound;

    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.maxAngularVelocity = 1.5f;

        ai = GetComponent<AIPath>();
        ai.enabled = false;

        lapCounter = 1;
        checkPointCounter = 0;
    }

    void Update()
    {
        if (GameManager.instance.raceStarted)
        {
            ai.enabled = true;
            engineSource.enabled = true;
            if (lapCounter > 3)
            {
                GameManager.instance.RaceOver(false);
            }
        }
        else
        {
            ai.enabled = false;
            engineSource.enabled = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerCar" || collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "OpponentCar")
        {
            SoundManager.instance.PlaySound(soundSource, crashSound[Random.Range(0, 5)], false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "FinishLine")
        {

            if (checkPointCounter == GameManager.instance.checkPoints.Length)
            {
                lapCounter++;
                checkPointCounter = 0;

                //reset all the check points
                for (int i = 0; i < GameManager.instance.checkPoints.Length; i++)
                {
                    GameManager.instance.checkPoints[i].GetComponent<CheckPoint>().hasCheckedOpponent[carNumber - 1] = false;
                }

            } 
        }

    }

}
