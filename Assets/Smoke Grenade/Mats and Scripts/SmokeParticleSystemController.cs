using UnityEngine;
using System.Collections;
using Meta.WitAi;
using UnityEngine.SceneManagement;
public class SmokeParticleSystemController : MonoBehaviour
{
    private ParticleSystem ps;
    public GameObject Granade;
    bool isIgnited = false;
    bool leverIsDown = false;
    public AudioSource audioSource;
    public AudioSource audioSource2LoopSmoke;
    public GameObject GrabbableGameObject;
    public float initDelay = 0.3f, duration = 10f;
    public ParticleSystem beginingExplosion,whiteSmoke;
    public GameObject fracturedObj;

    public GameObject staticRing;
    public GameObject InteractableRing;

    void Start()
    {

        ps = whiteSmoke;
        if(fracturedObj!=null)fracturedObj.SetActive(false);
        setRingStatic();

        
    }
 
    public void IgnitionDetached()
    {
        isIgnited = true;
        //GrabbableGameObject.GetComponent<Rigidbody>().isKinematic = true;
        //GrabbableGameObject.GetComponent<Rigidbody>().useGravity = true;

    }
    public void SmokeStart()
    { 
        if (isIgnited)
         StartCoroutine(enumerator());
    }
    public void SmokeStop()
    {
        ps.Stop();
    }


    public void LeverHolding()
    {
        leverIsDown = true;
        setRingInteractable();

    }
    public void LeverReleased()
    {
        leverIsDown = false;
        setRingStatic();
    }
    void setRingInteractable()
    {
        staticRing.SetActive(false);
        InteractableRing.SetActive(true);

    }
    void setRingStatic()
    {
        if (isIgnited) return;
        staticRing.SetActive(true);
        InteractableRing.SetActive(false);
    }
    IEnumerator enumerator()
    {
        GrabbableGameObject.GetComponent<Rigidbody>().isKinematic = false;
        GrabbableGameObject.GetComponent<Rigidbody>().useGravity = true;
        
        yield return new   WaitForSeconds(initDelay);

        ps.Play();

        if (beginingExplosion != null)  beginingExplosion.Play(); 
        
        //bang
        if (audioSource != null) audioSource.Play();
        if (audioSource2LoopSmoke != null) audioSource2LoopSmoke.Play();

        if (Granade != null) Granade.gameObject.SetActive(false);

        if(fracturedObj != null) 
        if (fracturedObj != null)
        {
                fracturedObj.SetActive(true);
                FracturedExplosionEffect fracturedExplosionEffect = fracturedObj.GetComponent<FracturedExplosionEffect>();
                if (fracturedExplosionEffect!=null)
                    fracturedObj.GetComponent<FracturedExplosionEffect>().explode();
        }

        
        if (GrabbableGameObject != null)
        {        
            Rigidbody rb = GrabbableGameObject.GetComponent<Rigidbody>(); 
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        } 


        yield return new WaitForSeconds(1f);
        beginingExplosion.Stop();

        yield return new WaitForSeconds (duration);
        ps.Stop(); 
        GrabbableGameObject.SetActive(false);

        if (audioSource2LoopSmoke != null) audioSource2LoopSmoke.Stop();

        Destroy(GrabbableGameObject);
        Destroy(gameObject);
    }
    // on scene loading stops 
}
