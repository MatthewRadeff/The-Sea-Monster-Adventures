using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // player health
    [SerializeField]
    private float m_playerHealth = 100.0f;
    // player health text
    public Text m_healthText;

    public Image m_healthBar;
    private float m_tempHealth = 1.0f;


    // swim and rotate speed
    public float m_playerWalkingSpeed = 20.0f;
    public float m_playerRotationSpeed = 50.0f;

    // the rigid body of the player
    public Rigidbody m_rb;

    // spawner where the attack spawns
    [SerializeField]
    private GameObject m_spawner = null;

    // attack prefab
    [SerializeField]
    private GameObject m_attackPrefab = null;


    // make sure only 1 bullet is firing at a time
    [SerializeField]
    private bool m_limitsToOneBullet = true;

    public float m_yPos = -0.1f;


    public AudioClip hurtClip;

    // 'health'
    public AudioClip berryClip;

    public AudioClip attackClip;



    public AudioClip pokeballClip;

    private bool m_pokeBallPickup1 = false;
    public Text m_pb1Text;

    private bool m_pokeBallPickup2 = false;
    public Text m_pb2Text;


    private bool m_pokeBallPickup3 = false;
    public Text m_pb3Text;




    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_healthText = GameObject.Find("Canvas").transform.FindChild("healthtext").GetComponent<Text>();
        m_healthBar = GameObject.Find("Canvas").transform.FindChild("health bar").GetComponent<Image>();

        m_pb1Text = GameObject.Find("Canvas").transform.FindChild("pb1text").GetComponent<Text>();
        m_pb2Text = GameObject.Find("Canvas").transform.FindChild("pb2text").GetComponent<Text>();
        m_pb3Text = GameObject.Find("Canvas").transform.FindChild("pb3text").GetComponent<Text>();

    }


    void Update()
    {
        float m_hor = Input.GetAxis("Horizontal");
        float m_ver = Input.GetAxis("Vertical");

  
        if(m_yPos <=0)
        {
            m_rb.transform.Translate(0, m_yPos * -1.0f, m_ver * m_playerWalkingSpeed * Time.deltaTime);
            m_yPos += 0.1f;
        }
        if(m_yPos >= 0)
        {
            m_rb.transform.Translate(0, m_yPos, m_ver * m_playerWalkingSpeed * Time.deltaTime);
            m_yPos -= 0.1f;
        }
        //m_rb.transform.Translate(0, m_yPos, m_ver * m_playerWalkingSpeed * Time.deltaTime);
        m_rb.transform.Rotate(0, m_hor * m_playerRotationSpeed * Time.deltaTime, 0);


        Attack();
        //UpdateSin();

    }



    private void OnTriggerEnter(Collider other)
    {
        // if player gets hit by enemy attack
        if(other.gameObject.CompareTag("EnemyAttack"))
        {
            m_playerHealth -= 0.05f * 100f;
            m_healthText.text = m_playerHealth.ToString("f0");

            m_tempHealth -= 0.05f;
            m_healthBar.fillAmount = m_tempHealth;


            AudioSource.PlayClipAtPoint(hurtClip, transform.position);

            //Debug.Log(m_playerHealth);

            if (m_playerHealth == 0)
            {
                SceneManager.LoadScene("gameover");
            }
        }
        if(other.gameObject.CompareTag("Berry"))
        {
            if(m_playerHealth != 0 && m_playerHealth != 100 && m_playerHealth < 100)
            {
                Destroy(other.gameObject);

                m_playerHealth += 0.05f * 100f;
                m_healthText.text = m_playerHealth.ToString("f0");

                m_tempHealth += 0.05f;
                m_healthBar.fillAmount = m_tempHealth;


                AudioSource.PlayClipAtPoint(berryClip,transform.position);

            }
        }
        if(other.gameObject.CompareTag("pokeball1"))
        {
            Destroy(other.gameObject);
            m_pokeBallPickup1 = true;

            m_pb1Text.color = Color.green;
            m_pb1Text.text = "Master Ball Picked Up";

            AudioSource.PlayClipAtPoint(pokeballClip, transform.position);


        }
        if (other.gameObject.CompareTag("pokeball2"))
        {
            Destroy(other.gameObject);
            m_pokeBallPickup2 = true;

            m_pb2Text.color = Color.green;
            m_pb2Text.text = "Quick Ball Picked Up";

            AudioSource.PlayClipAtPoint(pokeballClip, transform.position);


        }
        if (other.gameObject.CompareTag("pokeball3"))
        {
            Destroy(other.gameObject);
            m_pokeBallPickup3 = true;

            m_pb3Text.color = Color.green;
            m_pb3Text.text = "Ultra Ball Picked Up";

            AudioSource.PlayClipAtPoint(pokeballClip, transform.position);


        }
        if ((m_pokeBallPickup1 && m_pokeBallPickup2 && m_pokeBallPickup3) == true)
        {
            Destroy(m_pb2Text);
            Destroy(m_pb3Text);

            m_pb1Text.color = Color.white;
            m_pb1Text.text = "Teleporter Is Up";

        }

        if (other.gameObject.CompareTag("portal"))
        {
            if( ( m_pokeBallPickup1 && m_pokeBallPickup2 && m_pokeBallPickup3) == true)
            {
                SceneManager.LoadScene("win");
            }
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("water"))
        {
            m_rb.AddForce(new Vector3(0, 10, 0));

        }
    }



    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            // creating the actual object
            GameObject m_player = Instantiate(m_attackPrefab) as GameObject;
            // spawning the attack in the correct place we want it to (position)
            // aswell as staying with the player when rotated (rotation)
            // so the spawner will always fire the way we want it to
            m_player.transform.position = m_spawner.transform.position;
            m_player.transform.rotation = m_spawner.transform.rotation;

            AudioSource.PlayClipAtPoint(attackClip, transform.position);

            m_limitsToOneBullet = false;
        }
        else
        {
            m_limitsToOneBullet = true;
        }

    }


    public void UpdateSin()
    {
        float m_yPos = 0.5f;

        // sin function
        m_rb.transform.Translate(0, -Time.fixedDeltaTime * m_yPos, 0);



    }






}
