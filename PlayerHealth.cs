using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0) Die();
    }

    void Die() {
        Debug.Log("Player Died");
        // 游戏结束逻辑
    }
}