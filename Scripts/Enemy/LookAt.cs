using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // speed of enemy turning
    public float m_speed = 3.0f;

    // the target the enemy wants to look at
    public GameObject m_target = null;

    // known position of player
    Vector3 m_lastKnownPosition = Vector3.zero;

    // the rotation
    Quaternion m_lookAtRotation;
    
    
    
    	
	void Update ()
    {
        if(m_target)
        {
            // seing if we have the position of the player to look at him
            if(m_lastKnownPosition != m_target.transform.position)
            {
                m_lastKnownPosition = m_target.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }
            // once it has found the player we start to rotate the enemy to look at the player
            if(transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, m_speed * Time.deltaTime);
            }

        }
	}

}
