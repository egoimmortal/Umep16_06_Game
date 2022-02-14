using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMtest : MonoBehaviour
{
    public enum aFSMState
    {
        Idel,
        MoveToTarget,
        Chase,
        Attack,
        Dead
    }

    public GameObject AIgo;
    public GameObject Target;
    public float MoveSpeed = 5;
    private aFSMState m_CurrentState;
    private float m_CurrentTime;
    private float m_IdelTime;
    private int m_CurrentWanderPt;
    private GameObject[] m_WanderPoints;

    void Start()
    {
        m_CurrentWanderPt = -1;
        m_WanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        m_IdelTime = Random.Range(3.0f, 5.0f);
    }

    void Update()
    {
        Wander();
        Follow();
        
    }

    void Wander()
    {
        if (m_CurrentTime > m_IdelTime)
        {
            m_CurrentTime = 0;
            int newPoint = Random.Range(0, m_WanderPoints.Length);
            if (m_CurrentWanderPt == newPoint)
            {
                return;
            }

            m_CurrentWanderPt = newPoint;
            Target = m_WanderPoints[newPoint];
        }
        else
        {
            m_CurrentTime += Time.deltaTime;
        }
    }

    void Follow()
    {
        Vector3 vec = Target.transform.position - AIgo.transform.position;
        float dist = vec.magnitude;
        Transform A = AIgo.transform;
        A.LookAt(Target.transform);
        A.position = A.position + A.forward * MoveSpeed * Time.deltaTime;
    }
}
