using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vcamera : MonoBehaviour
{
    public static GameObject cam1;
    public static GameObject cam2;
    public static GameObject cam3;

    public Transform tP1;
    public Transform tP2;
    public Transform tM1;
    public Transform tM2;

    public static Cinemachine.CinemachineVirtualCamera Cam3;

    void Start()
    {
        cam1 = GameObject.Find("vcam1");
        cam2 = GameObject.Find("vcam2");
        cam3 = GameObject.Find("vcam3");
    }

    void Update()
    {

        //if (PlayerUI.b_P1 == true)
        //{
        //    cam1.SetActive(true);
        //    cam2.SetActive(false);
        //    cam3.SetActive(false);
        //    //Cam1.m_LookAt = aa;
        //}
        //else if (PlayerUI.b_P2 == true)
        //{
        //    cam1.SetActive(false);
        //    cam2.SetActive(true);
        //    cam3.SetActive(false);
        //    //Cam1.m_LookAt = bb;
        //}
        //else
        //{
        //    cam1.SetActive(false);
        //    cam2.SetActive(false);
        //    cam3.SetActive(true);
        //}

    }
    public static void Cam1GO()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
    }
    public static void Cam2GO()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        cam3.SetActive(false);
    }
    public static void Cam3GO(Transform t)
    {
        cam1.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(true);
        Cam3.m_LookAt = t;
    }
    public static void Cam3GO()
    {
        cam1.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(true);
    }
}
