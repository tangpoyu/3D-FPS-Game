using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField]
    private float damage = 2f, radius = 1f;
    [SerializeField]
    private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if(hits.Length > 0)
        {
            hits[0].GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
