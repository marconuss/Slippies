using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SLPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject playerLines;

    private Rigidbody2D rb2D;

    private Vector2 direction = Vector2.zero;

    private bool isInSlowZone;

    private float highSnowLeftLimit = -2.0f;
    private float highSnowRightLimit = 2.0f;

    private float xLimitLeft = -6.0f;
    private float xLimitRight = 6.0f;

    private Vector3 initialPosition;

    private InputAction playerMove;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        SLPauseManager.instance.OnReset += OnReset;
    }
    private void OnEnable()
    {
        playerMove = SLGameManager.instance.inputActions.Player.Move;
        playerMove.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
        if (SLPauseManager.instance)
        {
            SLPauseManager.instance.OnReset -= OnReset;
        }
    }
    private void OnReset()
    {
        playerLines.transform.rotation = Quaternion.Euler(Vector3.zero);
        isInSlowZone = false;
        transform.position = initialPosition;
    }

    private void Update()
    {
        
        direction = new Vector2(playerMove.ReadValue<float>(), 0.0f);

        highSnowLeftLimit = FindXLimit(SLGameManager.instance.GetLeftLimit()).x;
        highSnowRightLimit = FindXLimit(SLGameManager.instance.GetRightLimit()).x;

        if (transform.position.x < highSnowLeftLimit)
        {
            isInSlowZone = true;
        }
        if (transform.position.x > highSnowRightLimit)
        {
            isInSlowZone = true;
        }
        else if (transform.position.x < highSnowRightLimit && transform.position.x > highSnowLeftLimit)
        {
            isInSlowZone = false;
        }
        if(transform.position.x < xLimitLeft)
        {
            transform.position = new Vector3(xLimitLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xLimitRight)
        {
            transform.position = new Vector3(xLimitRight, transform.position.y, transform.position.z);
        }
    }
    private void FixedUpdate()
    {
        if (!SLPauseManager.instance.IsPaused)
        {
            MovePlayer(direction);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        if (isInSlowZone)
        {
            SLGameManager.instance.gameSpeed = 1f;
        }
        else
        {
            SLGameManager.instance.gameSpeed = 2f;
        }

        rb2D.MovePosition((Vector2)transform.position + (direction * SLGameManager.instance.gameSpeed * Time.deltaTime));
        if (direction.x > 0.01f)
        {
            playerLines.transform.rotation = Quaternion.Euler(Vector3.forward * 20);
        }
        else if (direction.x < -0.01f)
        {
            playerLines.transform.rotation = Quaternion.Euler(Vector3.forward * -20);
        }
        else
        {
            playerLines.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    Vector3 FindXLimit(LineRenderer lineLimit)
    {
        Vector3[] linePositions = new Vector3[lineLimit.positionCount];
        lineLimit.GetPositions(linePositions);

        Vector3 closestPoint = linePositions[0];

        for (int i = 0; i < lineLimit.positionCount - 1; i++)
        {
            if (Mathf.Abs(transform.position.y - closestPoint.y) > Mathf.Abs(transform.position.y - linePositions[i + 1].y))
            {
                closestPoint = linePositions[i+1];
            }
        }
        return closestPoint;
    }

}
