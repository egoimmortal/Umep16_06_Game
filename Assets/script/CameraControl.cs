using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera m_MyCamera;             //宣告攝影機變數

    public float m_fRotateSensitive = 1.5f;     //攝影機轉動速度
    public float m_fMoveSpeed = 1.0f;           //攝影機移動速度

    [Header("Camera Properties")]
    public float DistanceAway = 1.2f;                     //how far the camera is from the player.
    public float DistanceUp = 5;                    //how high the camera is above the player

    public float minDistance = 1.2f;                //最小攝影機距離
    public float maxDistance = 1.8f;                //最大攝影機距離

    public float smooth = 4.0f;                    //how smooth the camera moves into place
    public float rotateAround = 70f;            //the angle at which you will rotate the camera (on an axis)
    [Header("Player to follow")]
    public Transform target;                    //設定攝影機跟隨的目標

    [Header("Layer(s) to include")]
    public LayerMask CamOcclusion;                //攝影機碰撞的layer

    RaycastHit hit; //儲存射線碰撞的物件

    float cameraHeight = 0f;
    float cameraPan = 0f;
    float camRotateSpeed = 180f;

    Vector3 camPosition;
    Vector3 camMask;
    Vector3 followMask;

    private float HorizontalAxis;
    private float VerticalAxis;

    void Start()
    {
        rotateAround = target.eulerAngles.y - 90f;
        DistanceAway = 2.5f;
    }

    void LateUpdate()
    {
        if (NewBehaviour.attacking == true)
        {
            Fight();
        }
        else
        {
            if (TotalStatic.bCameraMove)
            {
                HorizontalAxis = Input.GetAxis("Mouse X");
                VerticalAxis = Input.GetAxis("Mouse ScrollWheel");
            }
            Vector3 targetOffset = new Vector3(target.position.x, (target.position.y + 2f), target.position.z);
            Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan); //紀錄歐拉角
            Vector3 vectorMask = Vector3.one;
            Vector3 rotateVector = rotation * vectorMask;
            camPosition = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

            camMask = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

            occludeRay(ref targetOffset); //判斷射線碰撞
            smoothCamMethod(); //做攝影機smooth
            m_MyCamera.transform.LookAt(target); //攝影機看向目標

            if (rotateAround > 360) //限制旋轉角度
                rotateAround = 0f;
            else if (rotateAround < 0f)
                rotateAround = (rotateAround + 360f);

            rotateAround += HorizontalAxis * camRotateSpeed * Time.deltaTime; //修正攝影機垂直距離
                                                                                //DistanceUp = Mathf.Clamp(DistanceUp += customPreferences.VerticalAxis2, -0.79f, 2.3f);
            DistanceAway = Mathf.Clamp(DistanceAway += VerticalAxis, minDistance, maxDistance * 2); //修正攝影機水平距離
        }
    }

    void smoothCamMethod()
    {
        smooth = 4f;
        m_MyCamera.transform.position = Vector3.Lerp(m_MyCamera.transform.position, camPosition, Time.deltaTime * smooth); //兩個值中間的差值
    }

    void occludeRay(ref Vector3 targetFollow)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            smooth = 10f;
            camPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f); //修正攝影機位置
        }
    }

    void Fight()
    {
        m_MyCamera.transform.LookAt(target);
        m_MyCamera.transform.Translate(new Vector3(0, 0, Time.deltaTime * 5f), Space.Self);
    }
}