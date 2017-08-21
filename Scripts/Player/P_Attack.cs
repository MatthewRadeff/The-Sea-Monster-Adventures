using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : MonoBehaviour
{

    // the speed at which the projectile is moving
    public float m_projectileSpeed = 80.0f;

    private void FixedUpdate()
    {
        // in the fixed update the projectile will be constant
        transform.Translate(0, 0, m_projectileSpeed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // projectile collding with the enemy it will cause it to destroy itself and the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Environment"))
        {
            Destroy(this.gameObject);
        }
    
    }


}
