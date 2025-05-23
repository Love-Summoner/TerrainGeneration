using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_rot : MonoBehaviour
{
    [SerializeField] Transform tracker;
    public float sensx;
    public float sensy;

    private GameObject Player;
    private Player_movement movement;
    public bool stop = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Player = GameObject.Find("Player");
        movement = Player.GetComponent<Player_movement>();
    }


    private float mousex = 0;
    private float mousey = 0;
    private float rotx = 0;
    private float roty = 0;
    private RaycastHit hit;
    void Update()
    {
        transform.position = tracker.position;
        mousex = Input.GetAxisRaw("Mouse X") * sensx * Time.deltaTime;
        mousey = Input.GetAxisRaw("Mouse Y") * sensy * Time.deltaTime;

        rotx -= mousey;
        roty += mousex;

        rotx = Mathf.Clamp(rotx, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotx, roty, 0);

    }
}
