using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanNPC : MonoBehaviour
{
    private GameObject UChan;

    void Start()
    {
        UChan = GameObject.Find("TimeLineunitychan");
    }

    void Update()
    {
        Vector3 vec = this.transform.position - TotalStatic.AcqChan.transform.position;
        float dist = vec.magnitude;
        if (dist < 4)
        {
                UChan.GetComponent<Animator>().SetTrigger("Reload");
        }
        else
        {
            
        }
    }
}
