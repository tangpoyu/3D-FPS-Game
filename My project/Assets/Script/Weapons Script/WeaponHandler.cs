using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;

    private bool isZoomed = false;
    [SerializeField]
    private WeaponAim weaponAim;
    [SerializeField]
    private WeaponFireType weaponFireType;
    [SerializeField]
    private WeaponBulletType weaponBulletType;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reloadSound;

    public GameObject attackPoint;

    public Animator Animator { get => animator; set => animator = value; }
    public WeaponFireType WeaponFireType { get => weaponFireType; set => weaponFireType = value; }
    public WeaponBulletType WeaponBulletType { get => weaponBulletType; set => weaponBulletType = value; }
    public WeaponAim WeaponAim { get => weaponAim; set => weaponAim = value; }
    public bool IsZoomed { get => isZoomed; set => isZoomed = value; }
   

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Shoot()
    {
       animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool isAim)
    {
        animator.SetBool(AnimationTags.AIM_PARAMETER, isAim);
    }

    public void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    public void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void PlayReloadSound()
    {
        reloadSound.Play();
    }

    public void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    public void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy) attackPoint.SetActive(false);
    }

    
}
