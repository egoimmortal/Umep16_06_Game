using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float MoveSpeed = 5.0f;  //移動速度
    private Animator m_animator;    //動作
    public Camera m_camera;         //攝影機1
    public GameObject cam_ref;      //攝影機參考點
    private CharacterController m_cc;

    private GameObject partical;

    void Start()
    {
        m_cc = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (TotalStatic.bFight == false && TotalStatic.bTalking == false) MOVE();
        else
        {
            m_animator.SetBool("Run", false);
            //Sounds.Instance().StopEffect();//停止音效
        }
    }
    
    public void MOVE()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isMove = ((h != 0) || (v != 0));

        Vector3 inputDir = new Vector3(h, 0, v);
        if (inputDir.magnitude > 1.0f) inputDir.Normalize();
        float CameraY = m_camera.transform.rotation.eulerAngles.y; //取得攝影機的正Y軸
        inputDir = Quaternion.Euler(0, CameraY, 0) * inputDir; //旋轉

        m_cc.SimpleMove(inputDir * MoveSpeed);
        /*
        if (Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.D))
        {
            Sounds.Instance().PlayRun();//播放跑步音效
        }
        */

        //角色轉向
        if (isMove)
        {
            float t = (8 * Time.deltaTime);//旋轉速度
            Vector3 forward = Vector3.Slerp(this.transform.forward, inputDir, t);//球形插值
            this.transform.rotation = Quaternion.LookRotation(forward);//轉向
        }

        if (m_camera.isActiveAndEnabled && isMove) Cursor.visible = false;
        else if(!isMove) Cursor.visible = true;

        //if(!m_camera.isActiveAndEnabled) Cursor.visible = false;

        

        m_animator.SetBool("Run", isMove);
    }
}















