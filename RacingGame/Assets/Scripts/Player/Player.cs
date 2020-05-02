using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    const float SPEED = 9000f;
    const float TURNSPEED = 15000f;
    private float powerInput;
    private float turnInput;
    bool brake;
    private Rigidbody carRigidbody;

    public int lapCounter;
    public int checkPointCounter;

    public bool onRoad;

    public int place;

    public AudioSource soundSource;
    public AudioSource engineSource;
    public AudioClip[] crashSound;

    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.maxAngularVelocity = 2.5f;

        lapCounter = 1;
        checkPointCounter = 0;
    }

    void Update()
    {
        if (GameManager.instance.raceStarted)
        {

            powerInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.Space))
            {
                brake = true;
            }
            else
            {
                brake = false;
            }

        }
        else
        {
            brake = true;
        }

        if (!onRoad)
        {
            carRigidbody.drag = 5;
        }
        else
        {
            carRigidbody.drag = 1;
        }
        Debug.Log(transform.eulerAngles);
        if ((transform.eulerAngles.z < 280 && transform.eulerAngles.z > 40) || (transform.eulerAngles.x < 280 && transform.eulerAngles.x > 40))
        {
            enabled = false;
            StartCoroutine(Crashed());
        }

    }

    void FixedUpdate()
    {

        if (!brake)
        {
            if(powerInput != 0)
            {
                if (!SoundManager.instance.engineIsOn)
                {
                    SoundManager.instance.StartOrStopEngine(engineSource, true);
                }                
            }
            else
            {

                    SoundManager.instance.StartOrStopEngine(engineSource, false);
                
                
            }
            carRigidbody.AddRelativeForce(-powerInput * SPEED * Time.deltaTime, 0f, 0f);
        }
        if (carRigidbody.velocity.magnitude > 10f)
        {
            carRigidbody.AddRelativeTorque(0f, turnInput * TURNSPEED * Time.deltaTime, 0f);
        }


    }

    IEnumerator Crashed()
    {
        yield return new WaitForSeconds(3);

        if ((transform.eulerAngles.z < 210 && transform.eulerAngles.z > 40) || (transform.eulerAngles.x < 210 && transform.eulerAngles.x > 40))
        {

           
            GameManager.instance.RaceOver(false);
        }
        else
        {
            enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OpponentCar" || collision.gameObject.tag == "Obstacle")
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
                    GameManager.instance.checkPoints[i].GetComponent<CheckPoint>().hasCheckedPlayer = false;
                }

            }
        }

        if(collision.tag == "Road")
        {
            onRoad = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Road")
        {
            onRoad = true;
        }
    }

    private void OnTriggerExit (Collider collision)
    {
        if(collision.tag == "Road")
        {
            onRoad = false;
        }
    }
}
