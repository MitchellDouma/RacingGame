using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public GameObject target;

    public GameObject[] wayPoints;

    public int currentPoint = 0;

    public int targetCar;

    void Update()
    {
        target.transform.position = wayPoints[currentPoint].transform.position;
    }

    IEnumerator OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "OpponentCar")
        {
            if(collision.GetComponent<Opponent>().carNumber == targetCar)
            {
                GetComponent<BoxCollider>().enabled = false;
                currentPoint++;
                if (currentPoint == 15)
                {
                    currentPoint = 0;
                }
                yield return new WaitForSeconds(1);
                GetComponent<BoxCollider>().enabled = true;
            }
           
        }
    }
}
