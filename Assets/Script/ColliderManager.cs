using UnityEngine;
using System.Collections;

public class ColliderManager : MonoBehaviour
{
    public string Info;
    public bool isDoubleFace=false;
    public Manager managerObj;
    public Rigidbody _rig;
	// Use this for initialization

    public void OnTriggerEnter(Collider col)
    {

        print(col.tag);
            if (col.CompareTag("playerCar")) 
            {

                _rig = col.transform.parent.parent.GetComponent<Rigidbody>();
                if (isDoubleFace)  
               {
                  managerObj.ShowInfo(Info);
                  return;
                }

            if (Mathf.Abs(transform.forward.y) > 0.8f)
            {
                if (transform.forward.y * _rig.velocity.y > 0)
                    managerObj.ShowInfo(Info);
            }

            if (Mathf.Abs(transform.forward.x) > 0.8f)
            {
                if (transform.forward.x * _rig.velocity.x > 0)
                    managerObj.ShowInfo(Info);
            }

            if (Mathf.Abs(transform.forward.z) > 0.8f)
            {
                if (transform.forward.z * _rig.velocity.z > 0)
                    managerObj.ShowInfo(Info);

            }
            if (Info == "5")
            {
                GetComponent<BoxCollider>().isTrigger = false;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
       
    }


    

}
