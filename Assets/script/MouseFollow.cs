using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{

    public GameObject m_NowTarget;
    private float m_MoveSpeed;
    private Animator anim;

    void Start()
    {
        m_MoveSpeed = 1.5f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_NowTarget != null)
        SeekMove();
       
    }

    private void SeekMove()
    {
        if (m_NowTarget == null)
        { return; }
        Vector3 gogo = this.transform.position;
        Vector3 vec = m_NowTarget.transform.position - this.transform.position;
        float dist = vec.magnitude;
        Transform A = this.transform;

        if(dist <= 3.0f)
        {
            A.LookAt(m_NowTarget.transform);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
        else if (dist < 5.0f && dist > 3.0f)
        {
            Vector3 t = m_NowTarget.transform.forward;
            t.y = 0;
            A.rotation = m_NowTarget.transform.rotation;
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            gogo = gogo + t * m_MoveSpeed * Time.deltaTime;
            A.position = gogo;
            m_MoveSpeed = 1.5f;
        }
        else
        {
            anim.SetBool("Walk", false);
            A.LookAt(m_NowTarget.transform);
            anim.SetBool("Run", true);
            gogo = gogo + A.forward * m_MoveSpeed * Time.deltaTime;
            A.position = gogo;
            m_MoveSpeed = 6;
        }
    }
}
