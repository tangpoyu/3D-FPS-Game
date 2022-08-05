using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;
    private int currentWeaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        int lastWeaponIndex = currentWeaponIndex;
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentWeaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentWeaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentWeaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentWeaponIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) currentWeaponIndex = 5;
        if (lastWeaponIndex != currentWeaponIndex)
        {
            weapons[lastWeaponIndex].gameObject.SetActive(false);
            weapons[currentWeaponIndex].gameObject.SetActive(true);
        }
    }
}
