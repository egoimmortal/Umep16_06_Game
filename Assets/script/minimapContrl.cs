using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapContrl : MonoBehaviour
{
    public GameObject gameplant;
    public GameObject minimapquad;
    public GameObject player;
    public GameObject miniplayer;
    public Camera minicamera;
    public GameObject playericon;
    public GameObject icon1;
    public GameObject icon2;
    public GameObject icon3;
    public GameObject icon4;
    public GameObject iconArrow;

    void Start()
    {
        iconArrow.SetActive(false);
    }

    void Update()
    {
        updateplayer();
        FarAway();
    }

    public void MiniPlus()
    {
        minicamera.orthographicSize += 5;
        playericon.transform.localScale += new Vector3(0.4f, 0.4f, 1f);
        icon1.transform.localScale += new Vector3(1.5f, 1.5f, 1f);
        icon2.transform.localScale += new Vector3(0.7f, 0.7f, 1f);
        icon3.transform.localScale += new Vector3(0.7f, 0.7f, 1f);

        if (minicamera.orthographicSize > 130)
        {
            minicamera.orthographicSize = 130;
            playericon.transform.localScale = new Vector3(6.9f, 6f, 1f);
            icon1.transform.localScale = new Vector3(34, 34, 1f);
            icon2.transform.localScale = new Vector3(12.5f, 12.5f, 1f);
            icon3.transform.localScale = new Vector3(11.5f, 11.5f, 1f);
        }
    }

    public void MiniSub()
    {
        minicamera.orthographicSize -= 5;
        playericon.transform.localScale -= new Vector3(0.4f, 0.4f, 1f);
        icon1.transform.localScale -= new Vector3(1.5f, 1.5f, 1f);
        icon2.transform.localScale -= new Vector3(0.7f, 0.7f, 1f);
        icon3.transform.localScale -= new Vector3(0.7f, 0.7f, 1f);

        if (minicamera.orthographicSize < 86)
        {
            minicamera.orthographicSize = 86;
            playericon.transform.localScale = new Vector3(4.5f, 4f, 1f);
            icon1.transform.localScale = new Vector3(25, 25, 1f);
            icon2.transform.localScale = new Vector3(8, 8, 1f);
            icon3.transform.localScale = new Vector3(7, 7, 1f);
        }
    }
    void FarAway()
    {
        if (icon1.activeSelf)
        {
            Vector3 ic1 = new Vector3(icon1.transform.position.x, 0, icon1.transform.position.z);
            Vector3 p = new Vector3(playericon.transform.position.x, 0, playericon.transform.position.z);
            Vector3 vec = ic1 - p;
            float dist = vec.magnitude;
            Vector3 v = vec.normalized;
            if (dist > 90)
            {
                iconArrow.SetActive(true);
                iconArrow.transform.position = playericon.transform.position + v * 70;
                iconArrow.transform.forward = icon1.transform.position;
            }
            else iconArrow.SetActive(false);
        }

        if (icon2.activeSelf)
        {
            Vector3 ic2 = new Vector3(icon2.transform.position.x, 0, icon2.transform.position.z);
            Vector3 p = new Vector3(playericon.transform.position.x, 0, playericon.transform.position.z);
            Vector3 vec = ic2 - p;
            float dist = vec.magnitude;
            Vector3 v = vec.normalized;
            if (dist > 90)
            {
                iconArrow.SetActive(true);
                iconArrow.transform.position = playericon.transform.position + v * 70;
                iconArrow.transform.forward = icon2.transform.position;
                iconArrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            else iconArrow.SetActive(false);
        }

        if (icon3.activeSelf)
        {
            Vector3 ic3 = new Vector3(icon3.transform.position.x, 0, icon3.transform.position.z);
            Vector3 p = new Vector3(playericon.transform.position.x, 0, playericon.transform.position.z);
            Vector3 vec = ic3 - p;
            float dist = vec.magnitude;
            Vector3 v = vec.normalized;
            if (dist > 90)
            {
                iconArrow.SetActive(true);
                iconArrow.transform.position = playericon.transform.position + v * 70;
                iconArrow.transform.forward = icon3.transform.position;
                iconArrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            else iconArrow.SetActive(false);
        }

        if (icon4.activeSelf)
        {
            Vector3 ic4 = new Vector3(icon4.transform.position.x, 0, icon4.transform.position.z);
            Vector3 p = new Vector3(playericon.transform.position.x, 0, playericon.transform.position.z);
            Vector3 vec = ic4 - p;
            float dist = vec.magnitude;
            Vector3 v = vec.normalized;
            if (dist > 85)
            {
                iconArrow.SetActive(true);
                iconArrow.transform.position = playericon.transform.position + v * 70;
                iconArrow.transform.forward = icon4.transform.position;
                iconArrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            else iconArrow.SetActive(false);
        }

        if (!icon3.activeSelf && !icon2.activeSelf && !icon1.activeSelf && !icon4.activeSelf)
        iconArrow.SetActive(false);

    }

    void updateplayer()
    {
        Vector3 pos = player.transform.position;
        float cy = minicamera.transform.position.y;
        float my = miniplayer.transform.position.y;

        pos.y = my;
        miniplayer.transform.position = pos;
        pos.y = cy;
        minicamera.transform.position = pos;
    }
}
