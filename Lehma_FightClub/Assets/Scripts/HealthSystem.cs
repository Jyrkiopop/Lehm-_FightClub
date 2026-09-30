using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Pelaajan asetukset")]
    public int playerID; // 1 Pelaajalle 1 (WASD), 2 Pelaajalle 2 (Nuolet)
    public float maxHealth = 100f;
    private float currentHealth;
    public Image healthBarImage;

    [Header("Hyökkäysasetukset (Voit muokata näitä Inspectorissa!)")]
    public Transform attackPoint;
    public float attackRange = 1.0f;
    public float attackDamage = 20f; // <-- TÄSTÄ voit säätää, kuinka paljon vahinkoa lyönti tekee
    public float attackAnimationDuration = 0.2f; // Kuinka kauan lyönti kestää
    public LayerMask playerLayer;

    [Header("Animaatio (Valinnainen)")]
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Pelaaja 1 käyttää E-näppäintä, Pelaaja 2 käyttää Right Control -näppäintä
        if (playerID == 1 && Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
        else if (playerID == 2 && Input.GetKeyDown(KeyCode.RightControl))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Animaatio
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
            Invoke("ResetAnimation", attackAnimationDuration);
        }

        if (attackPoint == null) return;

        // Etsitään hyökkäysalueelta toinen pelaaja
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D hit in hitPlayers)
        {
            // Varmistetaan, ettei lyö itseään
            if (hit.gameObject != gameObject)
            {
                PlayerHealth enemyHealth = hit.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    // Tehdään vahinkoa tällä inspectorissa säädetyllä damagella
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    void ResetAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Pelaaja " + playerID + " kuoli!");
    }

    // Piirtää ympyrän Scene-näkymään, jotta näet lyöntialueen
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}