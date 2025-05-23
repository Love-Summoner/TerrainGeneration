using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class camera_movement : MonoBehaviour
{
    public float sensx;
    public float sensy;
    public float speed;
    public bool stop = false;
    private Transform dir;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dir = transform.GetChild(0);
    }

    // Update is called once per frame

    private float mousex = 0;
    private float mousey = 0;
    private float rotx = 0;
    private float roty = 0;
    private RaycastHit hit;
    private Vector2 movement;
    void Update()
    {
        mousex = Input.GetAxisRaw("Mouse X") * sensx * Time.deltaTime;
        mousey = Input.GetAxisRaw("Mouse Y") * sensy * Time.deltaTime;

        rotx -= mousey;
        roty += mousex;

        rotx = Mathf.Clamp(rotx, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotx, roty, 0);
        
        transform.Translate(movement.x * speed * (transform.forward * movement.x).normalized * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Vertical");
        movement.y = Input.GetAxisRaw("Horizontal");
    }
}
