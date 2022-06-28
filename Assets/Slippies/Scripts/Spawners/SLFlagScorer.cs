using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLFlagScorer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "SLPlayer")
        {
            SLGameManager.instance.UpdateScore();
        }
    }

}
