using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DSteeringBehavior
{


    static public void Move(MapAIData data)
    {
        if (data.m_bMove == false)
        {
            return;
        }
        Transform t = data.AIgo.transform;
        Vector3 cPos = data.AIgo.transform.position;
        Vector3 vR = t.right;
        Vector3 vOriF = t.forward;
        Vector3 vF = data.m_vCurrentVector;

        if (data.m_fTempTurnForce > data.m_fMaxRot)
        {
            data.m_fTempTurnForce = data.m_fMaxRot;
        }
        else if (data.m_fTempTurnForce < -data.m_fMaxRot)
        {
            data.m_fTempTurnForce = -data.m_fMaxRot;
        }

        vF = vF + vR * data.m_fTempTurnForce;
        vF.Normalize();
        t.forward = vF;


        data.m_Speed = data.m_Speed + data.m_fMoveForce * Time.deltaTime;
        if (data.m_Speed < 0.01f)
        {
            data.m_Speed = 0.01f;
        }
        else if (data.m_Speed > data.m_fMaxSpeed)
        {
            data.m_Speed = data.m_fMaxSpeed;
        }

       
            if (data.m_Speed < 0.02f)
            {
                if (data.m_fTempTurnForce > 0)
                {
                    t.forward = vR;
                }
                else
                {
                    t.forward = -vR;
                }

            }
        

        cPos = cPos + t.forward * data.m_Speed;
        t.position = cPos;
    }

    

    static public bool Seek(MapAIData data)
    {
        Vector3 cPos = data.AIgo.transform.position;
        Vector3 vec = data.m_vTarget - cPos;
        vec.y = 0.0f;
        float fDist = vec.magnitude;
        if (fDist < data.m_Speed + 0.001f)
        {
            Vector3 vFinal = data.m_vTarget;
            vFinal.y = cPos.y;
            data.AIgo.transform.position = vFinal;
            data.m_fMoveForce = 0.0f;
            data.m_fTempTurnForce = 0.0f;
            data.m_Speed = 0.0f;
            data.m_bMove = false;
            return false;
        }
        Vector3 vf = data.AIgo.transform.forward;
        Vector3 vr = data.AIgo.transform.right;
        data.m_vCurrentVector = vf;
        vec.Normalize();

        float fDotF = Vector3.Dot(vf, vec);
        if (fDotF > 0.96f)
        {
            fDotF = 1.0f;
            data.m_vCurrentVector = vec;
            data.m_fTempTurnForce = 0.0f;
            data.m_fRot = 0.0f;
        }
        else
        {
            if (fDotF < -1.0f)
            {
                fDotF = -1.0f;
            }
            float fDotR = Vector3.Dot(vr, vec);

            if (fDotF < 0.0f)
            {

                if (fDotR > 0.0f)
                {
                    fDotR = 1.0f;
                }
                else
                {
                    fDotR = -1.0f;
                }

            }
            if (fDist < 3.0f)
            {
                fDotR *= (fDist / 3.0f + 1.0f);
            }
            data.m_fTempTurnForce = fDotR;

        }

        if (fDist < 10.0f)
        {
            Debug.Log(data.m_Speed);
            if (data.m_Speed > 0.1f)
            {
                data.m_fMoveForce = -fDotF * 3.0f;
            }
            else
            {
                data.m_fMoveForce = fDotF;
            }


        }
        else
        {
            data.m_fMoveForce = fDotF * 10.0f;
        }
         


        data.m_bMove = true;
        return true;
    }
}
