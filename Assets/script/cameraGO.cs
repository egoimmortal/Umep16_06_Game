using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraGO : MonoBehaviour
{

    public GameObject player;
    public float mouseX = 0;
    public float mouseY = 0;
    public float mouseScroll = 0;
    public float mouse_total =0;
    void Start()
    {
        
    }

    void Update()
    {
        
        transform.LookAt(player.transform);
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll != 0)
        {
            mouse_total += mouseScroll;
            if (mouse_total > 2 || mouse_total < -2)
            {
                mouse_total -= mouseScroll;
                mouseScroll = 0;
            }
            transform.Translate(new Vector3(0, 0, mouseScroll*Time.deltaTime*100f),Space.Self);
        }

        //if(Input.GetMouseButton(1))
        //{
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

        //if (transform.position.y < 3 && mouseY > 0)
        //{
        //    mouseY = 0;
        //}

        transform.RotateAround(player.transform.position, player.transform.up, mouseX*100f * Time.deltaTime);
        transform.RotateAround(player.transform.position, -transform.right, mouseY*100f * Time.deltaTime);
        //}
    }
}
