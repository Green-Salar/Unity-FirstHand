using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FracturedExplosionEffect : MonoBehaviour
{
    public List<Rigidbody> fracturedExplosionParticles;
    public void Start()
    {
        //explode();
    }
    public void explode()
    {
        StartCoroutine(enumerator());   
    }
    IEnumerator enumerator()
    {
        //yield return new WaitForSeconds(0.3f);
        foreach (Rigidbody rb in fracturedExplosionParticles)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(100, transform.position, 10);
            rb.useGravity = true;
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
