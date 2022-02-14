using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossFight : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        NewBehaviour.attacking = true;
        Destroy(gameObject);
    }
}