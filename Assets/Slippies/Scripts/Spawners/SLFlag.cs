using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLFlag : MonoBehaviour
{

    private List<Vector3> hitPositionList = new List<Vector3>()
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.5f, 0.5f, 0.0f),
        new Vector3(0.2f, 0.5f, 0.0f),
        new Vector3(0.2f, 0.2f, 0.0f),
    };

    private List<Vector3> startPositionList = new List<Vector3>()
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(-0.4f, 0.8f, 0.0f),
        new Vector3(0.0f, 0.62f, 0.0f),
    };

    public LineRenderer line;
    private void OnEnable()
    {
        line.SetPositions(startPositionList.ToArray());
        transform.rotation = Quaternion.identity;
        transform.parent.localScale = new Vector3(SLGameManager.instance.flagFilp, transform.parent.localScale.y, transform.parent.localScale.z);
        SLGameManager.instance.flagFilp *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.position.y > transform.parent.position.y)
        {
            line.SetPositions(hitPositionList.ToArray());
            if (transform.parent.localScale.x < 0 && collision.transform.position.x < transform.parent.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (transform.parent.localScale.x > 0 && collision.transform.position.x > transform.parent.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
    }
}
