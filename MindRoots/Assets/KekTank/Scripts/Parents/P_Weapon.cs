using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class P_Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float Cooldown = 0.2f;
    float InBetweenBullets = 0;
    public int ClipAmmo = 10;
    int Ammo = 0;
    public float ReloadTime = 2;

    [Header("Weapon Details")]
    public GameObject Bullet = null;
    public bool HitScan = false;
    public bool Special = false;
    public Transform Barrel = null;


    private void Update()
    {
        if (InBetweenBullets > 0) InBetweenBullets -= Time.deltaTime;
        if (InBetweenBullets < 0) InBetweenBullets = 0;
    }

    public void Fire(Vector3 Direction)
    {
        if (InBetweenBullets == 0)
        {
            if (Ammo > 0)
            {
                if (HitScan)
                {
                    FireHitScan();
                }
                else
                {
                    ThrowProjectile(Direction);
                }

                Ammo--;
                InBetweenBullets = Cooldown;

            }
            else
            {
                Reload();
            }
        }

    }

    void ThrowProjectile(Vector3 dir)
    {
        GameObject b = Instantiate(Bullet);
        b.transform.position = Barrel.position;
        
        P_Bullet pb = b.GetComponent<P_Bullet>();
        pb.Direction = dir;

        b.GetComponent<Rigidbody>().velocity = dir * pb.Speed;

        //Debug.Log("Mermi atmalýydý");

    }

    void FireHitScan()
    {

    }

    public void Reload()
    {
        //play animation
        //Animasyon sonu clip yenile
        Ammo = ClipAmmo;
    }

}
