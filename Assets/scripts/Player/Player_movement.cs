using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed, ground_distance, jump_power;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform feet;

    public bool stop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private float horizontal = 0;
    private float vertical = 0;
    private Vector3 movedirection = Vector3.zero;

    void Update()
    {
        if (stop)
            return;

        speedlimit();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movedirection = cam.transform.forward * vertical + cam.transform.right * horizontal;
        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.y, 0);
        rb.velocity = new Vector3(movedirection.normalized.x * speed, rb.velocity.y, movedirection.normalized.z * speed);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    private void speedlimit()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatvel.sqrMagnitude > speed)
        {
            rb.velocity = new Vector3(movedirection.normalized.x * speed, rb.velocity.y, movedirection.normalized.z * speed);
        }
    }
    private void Jump()
    {
        if (is_grounded())
        {
            rb.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
        }
    }
    private bool is_grounded()
    {
        return Physics.Raycast(feet.position, Vector3.down, ground_distance, groundlayer);
    }
}
