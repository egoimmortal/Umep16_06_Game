using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseKnightFSM : MonoBehaviour
{
    public enum aFSMState
    {
        Idle,
        MoveToTarget,
        Chase,
        Attack,
        RunToPlayer
    }

    public GameObject m_AIGO;
    public GameObject m_Target;
    private bool m_Move;
    private float m_MoveSpeed;

    private aFSMState m_CurrentState;
    private float m_IdleTime;
    private float m_CurrentTime;
    private int m_CurrentWanderPT;
    private GameObject[] m_WanderPoints;
    //private List<GameObject> m_RealPoint;
    private GameObject m_CurrentTarget;

    private GameObject m_NowTarget;

    public float fAttackRange;
    public float fChaseRange;
    public float fSight;

    private Animator ani;
    private GameObject m_ExMark;

    private void Start()
    {
        m_CurrentState = aFSMState.Idle;
        m_Move = false;
        m_CurrentTime = 0.0f;
        m_IdleTime = Random.Range(3.0f, 5.0f);
        m_CurrentWanderPT = -1;
        m_WanderPoints = GameObject.FindGameObjectsWithTag("MousePoint");     
        m_MoveSpeed = 1.5f;
        ani = GetComponent<Animator>();
        m_ExMark = ResoucesManager.Instance().LoadGameObject("ExMark");
        m_ExMark.SetActive(false);

    }
    private void Update()
    {
        //Debug.Log(m_CurrentTime);
        //Debug.Log(m_CurrentState);
        //Debug.Log(m_RealPoint.Count);

        if (m_CurrentState == aFSMState.Idle)
        {
            bool bbAttack = false;
            m_CurrentTarget = CheckEnemyInSight(ref bbAttack);
            if (m_CurrentTarget != null)
            {
                StartCoroutine(EXMark());////////////////
                m_NowTarget = m_CurrentTarget;
                if (bbAttack)
                {
                    m_CurrentState = aFSMState.Attack;
                    ani.SetTrigger("Atk");
                }
                else
                {
                    //播動畫 拉鏡頭
                    m_AIGO.transform.LookAt(m_NowTarget.transform);
                    if (m_CurrentTime > 2.5f)
                    {
                        m_MoveSpeed = 6;
                        m_CurrentTime = 0.0f;
                        m_CurrentState = aFSMState.Chase;
                        ani.SetBool("Run", true);
                    }
                    else m_CurrentTime += Time.deltaTime;
                }
                return;
            }
            if (m_CurrentTime > m_IdleTime)
            {
                m_CurrentTime = 0.0f;
                int newPT = Random.Range(0, m_WanderPoints.Length);
                if (m_CurrentWanderPT == newPT)
                { return; }
                m_IdleTime = 0.5f;
                m_CurrentWanderPT = newPT;
                m_NowTarget = m_WanderPoints[newPT];
                m_CurrentState = aFSMState.MoveToTarget;
                ani.SetBool("Walk", true);
                m_MoveSpeed = 1.5f;
            }
            else
                m_CurrentTime += Time.deltaTime;
        }

        else if (m_CurrentState == aFSMState.MoveToTarget)
        {
            bool bbAttack = false;
            m_CurrentTarget = CheckEnemyInSight(ref bbAttack);
            if (m_CurrentTarget != null)
            {
                StartCoroutine(EXMark());////////////////
                m_NowTarget = m_CurrentTarget;
                if (bbAttack)
                {
                    m_CurrentState = aFSMState.Attack;
                    ani.SetTrigger("Atk");
                }
                else
                {
                    m_AIGO.transform.LookAt(m_NowTarget.transform);
                    if (m_CurrentTime > 2.5f)
                    {
                        m_CurrentTime = 0.0f;
                        m_CurrentState = aFSMState.Chase;
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", true);
                        m_MoveSpeed = 6;
                    }
                    else m_CurrentTime += Time.deltaTime;
                }
                return;
            }

            SeekMove();

            if (m_Move == false)
            {
                m_CurrentState = aFSMState.Idle;
                m_CurrentTime = 0.0f;
                m_IdleTime = Random.Range(3.0f, 5.0f);
                ani.SetBool("Run", false);
                ani.SetBool("Walk", false);
            }
        }
        else if (m_CurrentState == aFSMState.Chase)
        {
            
            bool bbAttack = false;
            bool bCheck = CheckEnemyInSight(m_CurrentTarget, ref bbAttack);
            if (bCheck == false)
            {
                
                m_CurrentState = aFSMState.Idle;
                m_CurrentTime = 0.0f;
                m_IdleTime = Random.Range(3.0f, 5.0f);

                ani.SetBool("Run", false);
                ani.SetBool("Walk", false);
                return;
            }
            if (bbAttack)
            {
                m_CurrentState = aFSMState.Attack;
                ani.SetBool("Run", false);
                ani.SetTrigger("Atk");
            }
            else
            {
                m_NowTarget = m_Target;
                SeekMove();
                ani.SetBool("Run", true);
                m_MoveSpeed = 6;
            }
        }
        else if (m_CurrentState == aFSMState.Attack)
        {
            bool bbAttack = false;
            bool bCheck = CheckEnemyInSight(m_CurrentTarget, ref bbAttack);
            if (bCheck == false)
            {
                m_CurrentState = aFSMState.Idle;
                m_CurrentTime = 0.0f;
                m_IdleTime = Random.Range(3.0f, 5.0f);
                ani.SetBool("Run", false);
                return;

            }
            if (bbAttack == false)
            {
                m_NowTarget = m_CurrentTarget;
                m_CurrentState = aFSMState.Chase;
                ani.SetBool("Run", true);
                m_MoveSpeed = 6;
                return;
            }
            ani.SetTrigger("Atk");
        }
    }

    private GameObject CheckEnemyInSight(ref bool bAttack)
    {
        GameObject player = m_Target;
        Vector3 vec = player.transform.position - m_AIGO.transform.position;
        float dist = vec.magnitude;

        if (dist < fAttackRange)
        {
            bAttack = true;
            return player;
        }
        else if (dist < fSight)
        {
            bAttack = false;
            return player;
        }
        return null;
    }
    private bool CheckEnemyInSight(GameObject target, ref bool bAttack)
    {
        GameObject player = target;
        Vector3 vec = player.transform.position - m_AIGO.transform.position;
        float dist = vec.magnitude;

        if (dist < fAttackRange)
        {
            bAttack = true;
            return true;
        }
        else if (dist < fSight)
        {
            bAttack = false;
            return true;
        }
        return false;
    }

    private void SeekMove()
    {
        if (m_NowTarget == null)
        { return; }

        m_Move = true;
        Vector3 gogo = m_AIGO.transform.position;
        Vector3 vec = m_NowTarget.transform.position - m_AIGO.transform.position;
        float dist = vec.magnitude;
        Transform A = m_AIGO.transform;

        A.LookAt(m_NowTarget.transform);

        gogo = gogo + A.forward * m_MoveSpeed * Time.deltaTime;
        A.position = gogo;

        if (dist < 1)
            m_Move = false;
    }

    IEnumerator EXMark()
    {
        m_ExMark.SetActive(true);
        Vector3 ai = m_AIGO.transform.position;
        m_ExMark.transform.position = new Vector3(ai.x, ai.y + 3, ai.z);
        yield return new WaitForSeconds(0.5f);
        m_ExMark.SetActive(false);
    }



    private void OnDrawGizmos()
    {

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.transform.forward * 2.0f);
        if (m_CurrentState == aFSMState.Idle)
        {
            Gizmos.color = Color.green;
        }
        else if (m_CurrentState == aFSMState.MoveToTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, m_NowTarget.transform.position);
        }
        else if (m_CurrentState == aFSMState.Chase)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, m_NowTarget.transform.position);
        }
        else if (m_CurrentState == aFSMState.Attack)
        {
            Gizmos.color = Color.red;
        }
       
        Gizmos.DrawWireSphere(this.transform.position, fSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, fAttackRange);
    }


}
