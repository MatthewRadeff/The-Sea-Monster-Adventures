using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // prefab for bullet to be spawned
    [SerializeField]
    private GameObject m_enemyAttackPrefab = null;


    // spawner
    [SerializeField]
    private GameObject m_spawner = null;


    // seconds to spawn bullet
    [SerializeField]
    private float m_spawnSeconds = 0.75f;


    // make sure only 1 bullet is firing at a time
    [SerializeField]
    private bool m_limitsToOneBullet = true;



    // setting up a coroutine so enemy attacks after set amount of seconds
    private IEnumerator EnemyAttackCoroutine()
    {
        while(true)
        {
            // creating the actual object
            GameObject m_enemy = Instantiate(m_enemyAttackPrefab) as GameObject;
            // spawning the attack in the correct place we want it to (position)
            // aswell as staying with the enemy when rotated (rotation)
            // so the spawner will always fire the way we want it to
            m_enemy.transform.position = m_spawner.transform.position;
            m_enemy.transform.rotation = m_spawner.transform.rotation;

            // we can add sounds here so that whenever the turret shoots the sound will be in sync

            Debug.Log("enemy firing");

            // returns the amount of seconds until the attack is fired again
            // we already declared a number so lets plug it in 
            yield return new WaitForSeconds(m_spawnSeconds);
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        // when the player hits this collider
        if(other.gameObject.CompareTag("Player"))
        {
            // start coroutine and make sure theres only 1 projectile attack
            StartCoroutine(EnemyAttackCoroutine());
            m_limitsToOneBullet = false;
            
        }

    }




    private void OnTriggerExit(Collider other)
    {
        // when player exits out of the collider
        if(other.gameObject.CompareTag("Player"))
        {
            // stop all coroutines on the enemy... in this case its only the firing 
            StopAllCoroutines();
            m_limitsToOneBullet = true;

        }
    }



}
