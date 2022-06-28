using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ArcadeCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private CinemachineVirtualCamera arcadeCam;

    private bool isAcadeCam = false;

    public void SwitchCamPriority()
    {
        if (!isAcadeCam)
        {
            playerCam.Priority = 0;
            arcadeCam.Priority = 1;
        }
        else
        {
            playerCam.Priority = 10;
            arcadeCam.Priority = 0;
        }

        isAcadeCam = !isAcadeCam;
    }

}
