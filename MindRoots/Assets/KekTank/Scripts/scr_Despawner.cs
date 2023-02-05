using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<scr_PlayerMachine>().DeathofPlayer();
        }
    }
}
