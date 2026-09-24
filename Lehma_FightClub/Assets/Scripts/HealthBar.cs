using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Terveysasetukset")]
    [Tooltip("Maksimiterveys")]
    public float maxHealth = 100f;

    [Header("Testaa liuttamalla livenä pelin aikana!")]
    [Range(0f, 100f)]
    [Tooltip("Muuta tätä arvoa Inspectorissa pelin aikana, niin palkki reagoi heti.")]
    public float currentHealth = 100f;

    [Header("UI Reference")]
    [Tooltip("Vedä tähän se punainen HealthBarFill -image")]
    public Image healthBarImage;

    private float previousHealth;

    void Start()
    {
        currentHealth = maxHealth;
        previousHealth = currentHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Tämä tarkistaa automattisesti, jos muutat currentHealth-arvoa Inspectorissa pelin aikana
        if (currentHealth != previousHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthBar();
            previousHealth = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        previousHealth = currentHealth;
    }

    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            // Muuttaa terveyden 0 ja 1 välille
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}