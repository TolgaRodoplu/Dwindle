using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPoint : MonoBehaviour
{
    public Transform player;
    private bool isOverLapping = false;

    private void Update()
    {
        if (isOverLapping)
        {
            Vector3 offset = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, offset);

            if (dotProduct < 0)
            {
                EventSystem.instance.CloseCloset();
                isOverLapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverLapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverLapping = false;
        }
    }
}
