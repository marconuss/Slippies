using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLSpawnable : MonoBehaviour
{
    private Vector3 direction = Vector3.up;
    private bool isAtTheEdge = false;

    private LineRenderer[] lines;
    private BoxCollider2D[] colliders;

    public float angle = 1;

    private void Start()
    {
        SLPauseManager.instance.OnReset += OnReset;
        lines = gameObject.GetComponentsInChildren<LineRenderer>();
        colliders = gameObject.GetComponentsInChildren<BoxCollider2D>();
    }

    private void OnDestroy()
    {
        if (SLPauseManager.instance)
        {
            SLPauseManager.instance.OnReset -= OnReset;
        }
    }

    private void OnReset()
    {
        gameObject.SetActive(false);
    }


    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            var step = SLGameManager.instance.gameSpeed * Time.deltaTime;
            transform.Translate(direction * step);

            if (transform.position.y > 0f)
            {
                isAtTheEdge = true;
            }

            if (isAtTheEdge)
            {
                foreach (var line in lines)
                {
                    line.sortingOrder = -1;
                }
                direction = Vector3.down;
                foreach (var collider in colliders)
                {
                    collider.enabled = false;
                }

                if (transform.position.y < -1.5f)
                {
                    gameObject.SetActive(false);
                    foreach (var line in lines)
                    {
                        line.sortingOrder = 1;
                    }
                    foreach (var collider in colliders)
                    {
                        collider.enabled = true;
                    }
                    isAtTheEdge = false;
                    direction = Vector3.up;
                }
            }
        }
    }
}
