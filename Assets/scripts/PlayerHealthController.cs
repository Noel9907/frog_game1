using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this; 
    }

    public int currentHealth, maxHealth;
    public float InvinsibilityLength = 1f;
    private float InvinsibilityCounter;
    public SpriteRenderer thSR;
    public Color normal, faded;
    private PlayerController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        UIController.instance.UpdateHealthDisplay(currentHealth,maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(InvinsibilityCounter > 0)
        {
            InvinsibilityCounter -= Time.deltaTime;


        }
        else if(InvinsibilityCounter <= 0)
        {
            thSR.color = normal;
        }
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.H)) {
            GetHealth(1);
        }
#endif

    }

    public void DamagePlayer()
    {
        if (InvinsibilityCounter <= 0)
        {
            currentHealth -= 1;
            InvinsibilityCounter = InvinsibilityLength;
            UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                gameObject.SetActive(false);


            }
            else
            {
                InvinsibilityCounter =InvinsibilityLength;

                thSR.color = faded;
                thePlayer.knockBack();
            }
        }
        



    }
    public void GetHealth(int amouttoadd)
    {
        currentHealth += amouttoadd;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
