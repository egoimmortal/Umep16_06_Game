using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviour : MonoBehaviour
{
    public enum aFSMState
    {
        Idle,
        MoveToTarget,
        Chase,
        Attack,
        Dead
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
    private List<GameObject> m_RealPoint;
    private GameObject m_CurrentTarget;

    private GameObject m_NowTarget;

    public float fAttackRange;
    public float fChaseRange;
    public float fSight;

    private Animator ani;
    private GameObject m_ExMark;
    static public bool attacking;
    public static string monster_type;

    private void Start()
    {
        m_CurrentState = aFSMState.Idle;
        m_Move = false;
        m_CurrentTime = 0.0f;
        m_IdleTime = Random.Range(2.5f, 4.0f);
        m_CurrentWanderPT = -1;
        m_WanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        m_RealPoint = new List<GameObject>();
        for (int i = 0; i < m_WanderPoints.Length; i++)
        {
            Vector3 wanderDist = m_WanderPoints[i].transform.position - m_AIGO.transform.position;
            float wDist = wanderDist.magnitude;
            if (wDist < 18)
            {
                m_RealPoint.Add(m_WanderPoints[i]);
            }
        }
        m_MoveSpeed = 4;
        ani = GetComponent<Animator>();

        
        //Object o = Resources.Load("ExMark");
        //m_ExMark = GameObject.Instantiate(o) as GameObject;
        m_ExMark = gameObject.transform.GetChild(2).gameObject;
        m_ExMark.SetActive(false);

    }
    private void Update()
    {
        if (m_CurrentState == aFSMState.Idle)//如果現在的狀態是Idle時
        {
            bool bbAttack = false;
            m_CurrentTarget = CheckEnemyInSight(ref bbAttack);
            if (m_CurrentTarget != null)//當前時間存在時
            {
                StartCoroutine(EXMark());//判斷黃色驚嘆號
                m_NowTarget = m_CurrentTarget;//現在目標 = 當前目標
                if (bbAttack)
                {
                    m_CurrentState = aFSMState.Attack;//現在狀態變成攻擊
                    ani.SetTrigger("Atk");//使用攻擊動作
                    attacking = true;//public static 的攻擊開關
                    //FakeFighting.Fighting();
                    //Destroy(this.gameObject, 3.0f);//在1秒後銷毀這個物件
                    Invoke("SetActive", 0.5f);
                    monster_type = gameObject.tag;
                }
                else
                {
                    m_AIGO.transform.LookAt(m_NowTarget.transform);//看向目標
                    if (m_CurrentTime > 2.0f)//時間為2秒以上時
                    {
                        //Debug.Log(m_CurrentTime);//DEBUG追的時間
                        m_CurrentTime = 0.0f;//時間設為0
                        m_CurrentState = aFSMState.Chase;//現在的狀態更改成追的狀態
                        ani.SetBool("Run", true);//更改成跑的動作
                    }
                    else m_CurrentTime += Time.deltaTime;//deltaTime的增加時間
                }
                return;
            }
            if (m_CurrentTime > m_IdleTime)//當前時間>Idle時間時
            {
                m_CurrentTime = 0.0f;
                int newPT = Random.Range(0, m_RealPoint.Count);
                if (m_CurrentWanderPT == newPT)
                { return; }
                m_IdleTime = 0.5f;
                m_CurrentWanderPT = newPT;
                m_NowTarget = m_RealPoint[newPT];
                m_CurrentState = aFSMState.MoveToTarget;//現在的狀態更改成MoveToTarget
                ani.SetBool("Run", true);//更改成跑的動作
            }
            else
                m_CurrentTime += Time.deltaTime;//deltaTime的增加時間
        }

        else if (m_CurrentState == aFSMState.MoveToTarget)//現在的狀態是MoveToTarget時
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
                    attacking = true;
                    //FakeFighting.Fighting();
                    //Destroy(this.gameObject, 3.0f);///////////////////
                    Invoke("SetActive", 0.5f);
                    monster_type = gameObject.tag;
                }
                else
                {
                    m_AIGO.transform.LookAt(m_NowTarget.transform);
                    if (m_CurrentTime > 2.0f)
                    {
                        Debug.Log(m_CurrentTime);
                        m_CurrentTime = 0.0f;
                        m_CurrentState = aFSMState.Chase;
                        ani.SetBool("Run", true);
                    }
                    else m_CurrentTime += Time.deltaTime;
                    //Debug.Log(m_CurrentTime);
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
            }
        }
        else if (m_CurrentState == aFSMState.Chase)//當前狀態更改為Chase
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
            if (bbAttack)
            {
                m_CurrentState = aFSMState.Attack;
                ani.SetBool("Run", false);
                ani.SetTrigger("Atk");
                attacking = true;
                //FakeFighting.Fighting();
                //Destroy(this.gameObject, 3.0f);///////////////////
                Invoke("SetActive", 0.5f);
                monster_type = gameObject.tag;
            }
            else
            {
                m_NowTarget = m_Target;
                SeekMove();
                ani.SetBool("Run", true);
            }
        }
        else if (m_CurrentState == aFSMState.Attack)//當前狀態更改成Attack
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
                return;
            }
            ani.SetTrigger("Atk");
            attacking = true;
            //FakeFighting.Fighting();
            //Destroy(this.gameObject, 3.0f);///////////////////
            Invoke("SetActive", 0.5f);
            monster_type = gameObject.tag;
        }
    }


    private GameObject CheckEnemyInSight(ref bool bAttack)
    {
        GameObject player = m_Target;
        Vector3 vec = player.transform.position - m_AIGO.transform.position;
        float dist = vec.magnitude;

        ///目標在攻擊範圍內就設bAttack為true，否的話就設為false
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
        if (m_NowTarget == null)//現在目標為null的話就跳出
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
        //怪物頭上的黃色驚嘆號出現在怪物頭上0.5秒後消失
        m_ExMark.SetActive(true);
        //Vector3 ai = m_AIGO.transform.position;
        //m_ExMark.transform.position = new Vector3(ai.x, ai.y + 3, ai.z);
        yield return new WaitForSeconds(0.7f);
        m_ExMark.SetActive(false);
    }

    void SetActive()
    {
        this.gameObject.SetActive(false);
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
        else if (m_CurrentState == aFSMState.Dead)
        {
            Gizmos.color = Color.gray;
        }
        Gizmos.DrawWireSphere(this.transform.position, fSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, fAttackRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, fChaseRange);
    }


}
