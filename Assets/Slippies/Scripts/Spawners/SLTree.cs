using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLTree : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SLPlayer")
        {
            if (collision.transform.position.y > transform.position.y)
            {
                SLGameManager.instance.gameOver = true;
                SLPauseManager.instance.IsPaused = true;

            }
        }
    }
}
