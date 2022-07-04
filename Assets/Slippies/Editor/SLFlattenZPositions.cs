using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SLFlattenZPositions : Editor
{
    // MenuItem makes the function avaialbe in the top bar
    [MenuItem("Sippies/FlatenZ")]
    public static void FlatenZ()
    {
        // Find all objects in the scene that have
        // a line renderer component

        object[] obj = FindObjectsOfType(typeof(LineRenderer));

        // for every line renderer get all positions and
        // overwrite the z-position with 0
        foreach (object o in obj)
        {
            LineRenderer line = (LineRenderer)o;
            if (line != null)
            {
                Vector3[] positions = new Vector3[line.positionCount];

                line.GetPositions(positions);
                for (int i = 0; i < line.positionCount; i++)
                {
                    positions[i].z = 0f;
                }
                line.SetPositions(positions);
            }
        }
    }
}
