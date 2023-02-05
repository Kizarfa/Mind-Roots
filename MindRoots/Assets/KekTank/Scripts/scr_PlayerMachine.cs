using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Moving

}

public class scr_PlayerMachine : MonoBehaviour
{
    public bool FreeToAct = true;

    [Header("Player Stats")]
    public int Health = 10; //860 - 1050
    public int currentHp = 1;
    public int speed = 1;

    [Header("Player Possesions")]
    public P_Weapon Weapon = null;
    public Transform Aim;

    private void OnEnable()
    {
        currentHp = Health;
    }

    private void Update()
    {
        if (FreeToAct)
        {
            if (Input.GetMouseButton(0))   
            {
                Vector3 dir = (Aim.position - transform.position).normalized;
                Weapon.Fire(dir * 100);
            }
        }

    }

    public void TakeHit(int Damage, Vector3 HitPos, int pushStrength = 200)
    {
        //Debug.Log("HIT ME!!!");
        currentHp -= Damage;

        GetComponent<Rigidbody>().AddExplosionForce(pushStrength, transform.position + (HitPos - transform.position).normalized, 5);
        
        if (currentHp <= 0)
        {
            currentHp = 0;
            DeathofPlayer();
            return;
        }
        FindObjectOfType<scr_HealthUI>().UpdateHealth(currentHp, Health);

    }

    public void DeathofPlayer()
    {
        FindObjectOfType<scr_PlayerStart>().SpawnAnotherPlayer();
    }

}
