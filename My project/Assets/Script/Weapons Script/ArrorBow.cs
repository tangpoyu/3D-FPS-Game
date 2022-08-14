using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrorBow : MonoBehaviour
{
    private Rigidbody myBody;

    [SerializeField]
    private float speed = 1f , deactivateTimer = 3f, damage;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
       Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateGameObject()
    {
        if(gameObject.activeInHierarchy)
        {
           Destroy(gameObject);
        }
    }

    public void Fire(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.name);
        if(other.tag == Tags.ENEMY_TAG)
        {
            other.GetComponent<HealthScript>().ApplyDamage(damage);
        }
    }
}
