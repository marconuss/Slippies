using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLSpawnable : MonoBehaviour
{
    private Vector3 direction = Vector3.up;
    private bool isAtEdge = false;

    private LineRenderer[] lines;
    private BoxCollider2D[] colliders;

    private float edgePosY = 0f;
    private float endPosY = -1.5f;

    private int backgroundOrder = -1;
    private int foregroundOrder = 1;

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

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            var step = SLGameManager.instance.gameSpeed * Time.deltaTime;
            transform.Translate(direction * step);

            if (transform.position.y > edgePosY)
            {
                if (!isAtEdge)
                {
                    OnEdgeReached();
                }
            }

            if (transform.position.y < endPosY)
            {
                if (isAtEdge)
                {
                    OnReset();
                }
            }
        }
    }
    void OnEdgeReached()
    {
        isAtEdge = true;
        foreach (var line in lines)
        {
            line.sortingOrder = backgroundOrder;
        }
        direction = Vector3.down;
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
    private void OnReset()
    {
        gameObject.SetActive(false);
        foreach (var line in lines)
        {
            line.sortingOrder = foregroundOrder;
        }
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        isAtEdge = false;
        direction = Vector3.up;
    }
}
