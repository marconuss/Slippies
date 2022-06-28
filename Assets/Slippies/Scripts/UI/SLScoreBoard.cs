using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLScoreBoard : MonoBehaviour
{
    public List<GameObject> digitPositionList;

    public void UpdateNumberUI(int score, int pos = 0)
    {
        Transform currentDecimmal = digitPositionList[pos].transform;
        for (int i = 0; i < currentDecimmal.childCount; i++)
        {
            if (i == (score % 10))
            {
                currentDecimmal.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                currentDecimmal.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (score >= 10 || (pos < digitPositionList.Count - 1 && score == 0))
        {
            UpdateNumberUI(score / 10, pos + 1);
        }
    }
    public void ResetNumberUI()
    {
        UpdateNumberUI(0);
    }
}
