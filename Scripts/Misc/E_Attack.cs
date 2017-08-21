using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Attack : MonoBehaviour
{
    // the speed at which the projectile is moving
    public float m_projectileSpeed = 50.0f;


    public float m_destroySeconds = 6.0f;


    private void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }


    private void FixedUpdate()
    {
        // in the fixed update the projectile will be constant
        transform.Translate(0,0, m_projectileSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // projectile collding with either the player or environment will cause it to destroy itself
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Environment"))
        {
            Destroy(this.gameObject);
        }
    }

}
