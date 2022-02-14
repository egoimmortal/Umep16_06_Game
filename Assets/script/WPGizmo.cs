using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 10);

    }
}
