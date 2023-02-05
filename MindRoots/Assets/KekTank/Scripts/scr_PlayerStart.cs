using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerStart : MonoBehaviour
{
    public GameObject player;

    public void SpawnAnotherPlayer()
    {
        GameObject olone = (FindObjectOfType<scr_PlayerMachine>().gameObject);

        GameObject p = Instantiate(player);
        p.transform.position = transform.position;

        scr_PlayerMachine pm = p.GetComponent<scr_PlayerMachine>();
        FindObjectOfType<scr_HealthUI>().UpdateHealth(pm.currentHp, pm.Health);

        Destroy(olone);
    }

}
