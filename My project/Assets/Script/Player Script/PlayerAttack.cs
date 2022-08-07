using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    private float fireRate = 15f;
    private float nextTimeToFire;
    private float damage = 20f;

    private Animator zoomCarmeraAnim;
    private Camera mainCam;
    private GameObject crosshair;

    [SerializeField]
    private GameObject arrowPrefab, spearPrefab;

    [SerializeField]
    private Transform arrowStartPosition;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomCarmeraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ZoomInAndOut();
        WeaponShoot();
    }

    private void ZoomInAndOut()
    {
        WeaponHandler weapon = weaponManager.SelectedWeapon;
        if (weapon.WeaponAim == WeaponAim.AIM)
        {
            
            if(Input.GetMouseButtonDown(1))
            {
                weapon.IsZoomed = !weapon.IsZoomed; 
            }
            zoomCarmeraAnim.Play(weapon.IsZoomed ? AnimationTags.ZOOM_IN_ANIM : AnimationTags.ZOOM_OUT_ANIM);
        }

        if(weapon.WeaponAim == WeaponAim.SELF_AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                weapon.IsZoomed = !weapon.IsZoomed;
            }
            weapon.Aim(weapon.IsZoomed);
        }
    }

    private void WeaponShoot()
    {
        WeaponHandler weapon = weaponManager.SelectedWeapon;
        if(weapon.WeaponFireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                weapon.Shoot();
                BulletFired();
            }
        } else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (weapon.tag == Tags.AXE_TAG)
                {
                    weapon.Shoot();
                }

                if (weapon.WeaponBulletType == WeaponBulletType.BULLET)
                {
                    weapon.Shoot();
                    BulletFired();
                } else
                {
                    if(weapon.IsZoomed)
                    {
                        weapon.Shoot();
                        weapon.IsZoomed = false;
                        
                        if(weapon.WeaponBulletType == WeaponBulletType.ARROW)
                        {
                            ThrowWeapon(Tags.ARROW_TAG);
                        } else if(weapon.WeaponBulletType == WeaponBulletType.SPEAR) {
                            ThrowWeapon(Tags.SPEAR_TAG);
                        }
                    }
                }
            }
        }
    }

    public void ThrowWeapon(string weaponType)
    {
        switch(weaponType)
        {
            case Tags.ARROW_TAG:
                GameObject arrow = Instantiate(arrowPrefab);
                arrow.transform.position = arrowStartPosition.position;
                arrow.GetComponent<ArrorBow>().Fire(mainCam);
                break;

            case Tags.SPEAR_TAG:
                GameObject spear = Instantiate(spearPrefab);
                spear.transform.position = arrowStartPosition.position;
                spear.GetComponent<ArrorBow>().Fire(mainCam);
                break;
        }
    }

    private void BulletFired()
    {
        RaycastHit hit;
            
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            print(hit.transform.gameObject.name);
        }
    }
}
