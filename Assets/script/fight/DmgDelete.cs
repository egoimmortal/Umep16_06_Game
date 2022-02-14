using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgDelete : MonoBehaviour
{
    private Vector3 tem_y;
    void Update()
    {
        tem_y = transform.position;
        tem_y.y += 40 * Time.deltaTime;
        transform.position = tem_y;

        Destroy(gameObject, 1.5f);
    }
}
