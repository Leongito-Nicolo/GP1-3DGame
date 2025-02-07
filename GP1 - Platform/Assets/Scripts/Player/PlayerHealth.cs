using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerMaxHealth;                               // player max health
    public float enemyDamage;                                   // enemy damage
    public float playerCurrentHealth;                           // player current health

    [SerializeField] private float recoilTime;                  // time in which the player is immune to damage after an hit
    public bool isInvincible = false;                           // is player invincible?

    [SerializeField] private UIManager uiManager;               // instance of the UI manager
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;      // set player's current health to the max health
    }

    // coroutine that reduce the health of the player when he takes damage
    public IEnumerator GetDamage(float damage)
    {
        isInvincible = true;        // set isInvincible to true

        playerCurrentHealth -= damage;      // reduce the health
        uiManager.UpdateHealthBar();        // update the healthbar

        yield return new WaitForSeconds(recoilTime);        // wait for recoilTime and can't be damaged

        isInvincible = false;       // set isInvincible to false
    }

    private void OnTriggerStay(Collider collider)
    {
        // check damage only if the player is not invincible
        if (!isInvincible)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                StartCoroutine(GetDamage(enemyDamage));
            }
        }
    }

}
