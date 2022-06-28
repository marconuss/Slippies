using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FlatenZPositions : Editor
{
    [MenuItem("Sippies/FlatenZ")]
    public static void FlatenZ()
    {

        object[] obj = FindObjectsOfType(typeof(LineRenderer));
        foreach (object o in obj)
        {
            LineRenderer line = (LineRenderer)o;
            if(line != null)
            {
                Vector3[] positions = new Vector3[line.positionCount];
                line.GetPositions(positions);
                for(int i= 0; i < line.positionCount; i++ )
                {
                    positions[i].z = 0f;
                }
                line.SetPositions(positions);
            }
        }
    }

}
