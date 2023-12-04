using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHealthBar : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, transform.position.z);
        transform.position = move;
    }
}
