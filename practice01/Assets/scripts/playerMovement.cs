using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 200f;
    public float sidewayForce = 200f;

 

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewayForce, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewayForce,0, 0);
        }

        
    }
}
