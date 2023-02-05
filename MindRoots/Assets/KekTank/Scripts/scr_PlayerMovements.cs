using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class scr_PlayerMovements : MonoBehaviour
{
    public bool FreeToMove = true;
    public Animator animator;

    [Header("Moving Parts")]
    public Transform MyEyes;
    public Vector2 Constraints = new Vector2(-45, 45);
    public float Sensitivity = 1.0f;

    Vector2 LastMPos = new Vector2();
    

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FreeToMove)
        {
            Move();
            Rotation();
        }
        else
        {

        }

    }

    void Rotation()
    {
        LastMPos.x += Input.GetAxis("Mouse X");
        LastMPos.y += Input.GetAxis("Mouse Y");
        LastMPos.y = Mathf.Clamp(LastMPos.y, Constraints.x, Constraints.y);

        transform.rotation = Quaternion.Euler(transform.rotation.x, LastMPos.x, transform.rotation.z);
        MyEyes.transform.localRotation = Quaternion.Euler(-LastMPos.y, MyEyes.transform.localRotation.y, MyEyes.transform.localRotation.z);
        //Debug.Log("check");
    }

    void Move()
    {
        float Dikey = Input.GetAxis("Vertical");//int Dikey = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);
        float Yatay = Input.GetAxis("Horizontal");//int Yatay = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);

        if (Dikey != 0 || Yatay != 0)
        {
            //Debug.Log("check move");
            float speed = gameObject.GetComponent<scr_PlayerMachine>().speed;
            Rigidbody rig = transform.GetComponent<Rigidbody>();
            rig.velocity = ((transform.forward * Dikey * speed) + (MyEyes.right * Yatay * speed)) + new Vector3(0, rig.velocity.y, 0);

            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
}
