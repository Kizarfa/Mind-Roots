using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ArenaStarter : MonoBehaviour
{
    public bool PlayerIsInside = false;
    public int ChildRots = 0;

    public GameObject Neuron;
    public Material RotMat;
    public Material CleanMat;

    private void OnEnable()
    {
        ChildRots = transform.GetComponentsInChildren<P_Enemy>().Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsInside = false;
        }
    }

    public void Spawn()
    {
        //spawn iþlemleri
        //---------------
        ChildRots++;
    }

    public void RotDecrease()
    {
        ChildRots--;
        if (ChildRots == 0) Cleanup();
    }

    public void Cleanup()
    {
        //temiz sinir hücresi ol
        Neuron.GetComponent<Renderer>().sharedMaterial = CleanMat;
    }
}
