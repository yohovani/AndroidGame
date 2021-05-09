using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    public Transform player0;

    void Update()
    {
        if (player0 != null)
        {
            Vector3 position = transform.position;
            position.x = player0.position.x;
            transform.position = position;
        }
    }
}
