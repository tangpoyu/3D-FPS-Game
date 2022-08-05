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

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
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

                }
            }
        }
    }

    private void BulletFired()
    {
        throw new NotImplementedException();
    }
}
