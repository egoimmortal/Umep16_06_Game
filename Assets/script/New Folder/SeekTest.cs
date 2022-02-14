using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekTest : MonoBehaviour
{
    public GameObject target;
    public MapAIData data;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        data.m_vTarget = target.transform.position;
        DSteeringBehavior.Seek(data);
        DSteeringBehavior.Move(data);
    }
}
