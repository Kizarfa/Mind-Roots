using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    [Header("Bullet stats")]
    public int Damage = 1;
    public float Speed = 1;
    public bool Mortal = true;
    
    [Header("Bullet moves")]
    public float LifeTime = 5;
    public Vector3 Direction = new Vector3();
    public float Zfall = 0.001f;

    bool hit = false;
    Vector3 HitPos = new Vector3();
    private float limit = 0.01f;
    private int cycles;
    private int cyclesToSleep = 10;
 
    void FixedUpdate()
    {
        Rigidbody rig = GetComponent<Rigidbody>();
        if (rig != null)
        {
            if (rig.velocity.sqrMagnitude > limit)
            {
                cycles = cyclesToSleep; // reload counter if velocity isn't negligible
            }
            if (cycles > 0)
            { // w$$anonymous$$le counter > 0 apply force
                cycles--;
                rig.AddForce(Zfall * Physics.gravity);
            }

        }

        if (Mortal)
        {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0) Disappear();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;
        HitPos = collision.contacts[0].point;
        //Destroy(GetComponent<Rigidbody>());
        HitEffect(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        hit = true;
        HitPos = other.ClosestPointOnBounds(transform.position);
        //Destroy(GetComponent<Rigidbody>());
        HitEffect(other.gameObject);
    }

    void HitEffect(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<P_Enemy>().TakeHit(Damage, HitPos);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<scr_PlayerMachine>().TakeHit(Damage, HitPos);
        }
        else
        {

        }

        //Debug.Log("Hit ya!");
        if (Mortal) Disappear();

    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
