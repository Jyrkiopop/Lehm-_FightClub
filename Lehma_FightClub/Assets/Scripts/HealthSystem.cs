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
    public float attackDamage = 20f;
    public float attackAnimationDuration = 0.2f;
    // Poistettu LayerMask, koska sitä ei enää tarvita!

    [Header("Animaatio (Valinnainen)")]
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Pelaaja 1 Lyö E näppäimellä
        if (playerID == 1 && Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

        // Pelaaja 2 Lyö oikea Ctrl
        else if (playerID == 2 && Input.GetKeyDown(KeyCode.RightControl))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
            Invoke("ResetAnimation", attackAnimationDuration);
        }

        if (attackPoint == null) return;

        // Haetaan kaikki colliderit annetulta alueelta ilman layer-suodatusta
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D hit in hitPlayers)
        {
            // Varmistetaan, että emme lyö itseämme
            if (hit.gameObject != gameObject)
            {
                PlayerHealth enemyHealth = hit.GetComponent<PlayerHealth>();

                // Jos osutulla objektilla on PlayerHealth-skripti, se ottaa vahinkoa
                if (enemyHealth != null)
                {
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}