using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            if (cube.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                cube.EndGame(false);
                Time.timeScale = 0f;
            }
                
        }
    }

}
