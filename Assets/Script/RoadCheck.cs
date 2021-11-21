using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCheck : MonoBehaviour
{
    [SerializeField] Manager manager;
    RaycastHit hit;
    bool isGround = true;
    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }
    void Update()
    {
        if (Physics.Raycast(transform.position + Vector3.up, -transform.up, out hit, 20))
        {
            CheckGround();
        }
    }

    ///<summary>
    ///Check if the vehicle is in the ground
    ///</summary>
    private void CheckGround()
    {
        if (hit.collider == null) return;
        if (hit.collider.CompareTag("ground"))
        {
            isGround = true;
        }
        else
        {
            if (isGround)
            {
                manager.ShowInfo("4");
                isGround = false;
            }
        }
    }
}