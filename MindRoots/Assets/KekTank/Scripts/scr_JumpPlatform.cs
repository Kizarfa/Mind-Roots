using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_JumpPlatform : MonoBehaviour
{
    public scr_JumpPlatform Partner;
    public AnimationCurve JumpCurve;

    public float Yoffset = 2;
    public float jumpTime = 0;
    public float jumpHigh = 5;
    public float AirTime = 2;

    public bool Transing = false;
    public bool JustPassed = false;

    private void Update()
    {
        if (Transing)
        {
            Transfering();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !JustPassed)
        {
            Transing = true;
            jumpTime = 0;

            JustPassed = true;
            Partner.JustPassed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            JustPassed = false;
        }
    }

    public void Transfering()
    {
        if (jumpTime == 0)
        {
            FindObjectOfType<scr_PlayerMachine>().FreeToAct = false;
            FindObjectOfType<scr_PlayerMovements>().FreeToMove = false;
        }

        jumpTime += Time.deltaTime / AirTime;
        if (jumpTime > 1) jumpTime = 1;

        Vector3 diff = Vector3.Lerp(transform.position + (Vector3.up * Yoffset), Partner.transform.position + (Vector3.up * Yoffset), jumpTime);

        Transform plyr = FindObjectOfType<scr_PlayerMachine>().gameObject.transform;
        plyr.position = diff + new Vector3(0, JumpCurve.Evaluate(jumpTime) * jumpHigh, 0);

        if (jumpTime == 1)
        {
            FindObjectOfType<scr_PlayerMachine>().FreeToAct = true;
            FindObjectOfType<scr_PlayerMovements>().FreeToMove = true;
            Transing = false;
        }
    }
}
