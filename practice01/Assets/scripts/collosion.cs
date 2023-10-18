
using UnityEngine;

public class collosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisioninfo)
    {
        if(collisioninfo.collider.name== "obstacle")
        {
            Debug.Log("We hit the obstacle");
        }
    }
}
