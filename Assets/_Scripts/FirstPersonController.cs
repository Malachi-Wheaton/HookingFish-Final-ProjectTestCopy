using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed  =   2.0f;
    public float jumpSpeed  =   2.0f;
    public float yawSpeed   =   260.0f;
    public float pitchSpeed =   260.0f;
    public float minPitch   =   -45.0f;
    public float maxPitch   =   45.0f;
    public Transform groundReference;

    private Rigidbody rb;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //put all input axis info into variable
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float yaw = Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y");

        Vector3 velo = Vector3.zero;
        velo += transform.right * h;
        velo += transform.forward * v;
        velo *= moveSpeed;

        //set the jump
        velo.y = rb.velocity.y;

        //if (Input.GetKeyDown(KeyCode.Space) &&
        //    //Physics.CheckSphere(groundReference.position, .04f))
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() )
        {
            velo.y = jumpSpeed;
        }

        //apply movement
        rb.velocity = velo;

        //apply rotation
        transform.localEulerAngles += new Vector3(
            0,
            yaw * yawSpeed * Time.deltaTime,
            0);

        float pitchDelta = -1 * pitch * pitchSpeed * Time.deltaTime;
        float newPitch = cameraTransform.localEulerAngles.x + pitchDelta;
        newPitch = angleWithin180(newPitch);

        //Debug.Log($"Pitch before: {newPitch}");
        newPitch = Mathf.Clamp(newPitch, minPitch, maxPitch);
        //Debug.Log($"Pitch after: {newPitch}");

        cameraTransform.localEulerAngles = new Vector3(newPitch,
            cameraTransform.localEulerAngles.y,
            cameraTransform.localEulerAngles.z);

        //TODO: add jump

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(groundReference.position, Vector3.down, .15f);
    }

    private float angleWithin180(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
        //this does the same thing as the ternary operator above
        //if (angle > 180) 
        //    return angle - 360;
        //else 
        //    return angle;
    }
}
