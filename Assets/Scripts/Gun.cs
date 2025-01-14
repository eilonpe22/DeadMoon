using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    [SerializeField] bool rapidFire = false;

    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] float fireRate = 5f;

    WaitForSeconds rapidFireWait;

    [SerializeField] int maxAmmo;
    int currentAmmo;

    [SerializeField] float reloadTime;
    WaitForSeconds reloadWait;

    public ParticleSystem muzzleFlash;
   
    AudioSource shootingSound;
    
    

    

    private void Awake()
    {
        shootingSound = GetComponent<AudioSource>();
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(0.5f / fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        
    }

    public void Shoot()
    {
        currentAmmo--; //currentAmmo = currentAmmo -1
        RaycastHit hit;
        muzzleFlash.Play();
        shootingSound.Play();
        
      
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamage(damage, hit.point, hit.normal);
            }
                
        }
       

    }
    
    public IEnumerator RapidFire()
    {
        if (CanShoot())
        {
            Shoot();
            if (rapidFire)
            {
                while (CanShoot())
                {
                    yield return rapidFireWait;
                    Shoot();
                }
                StartCoroutine(Reload());
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
       
    }

    IEnumerator Reload()
    {
        if(currentAmmo == maxAmmo)
        {
            yield return null;
        }

        print("reloading...");
        yield return reloadWait;
        currentAmmo = maxAmmo;
        print("finished reloading.");
    }

   

    bool CanShoot()
    {
        bool enoughAmmo = currentAmmo > 0;
        return enoughAmmo;
    }
}
