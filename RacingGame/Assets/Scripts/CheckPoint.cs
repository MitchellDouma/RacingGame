using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public bool hasCheckedPlayer;
    public bool[] hasCheckedOpponent;

    void Start()
    {
        hasCheckedOpponent = new bool[3];
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "OpponentCar")
        {
            if(!hasCheckedOpponent[collision.GetComponent<Opponent>().carNumber - 1])
            {
                hasCheckedOpponent[collision.GetComponent<Opponent>().carNumber - 1] = true;
                collision.GetComponent<Opponent>().checkPointCounter++;
            }
           

        }
        if(collision.tag == "Player")
        {
            if (!hasCheckedPlayer)
            {
                hasCheckedPlayer = true;
                collision.GetComponent<Player>().checkPointCounter++;
            }
            


        }
    }

}
