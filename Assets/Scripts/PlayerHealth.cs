using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Player starting health

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("damage"))
        {
            DecreaseHealth(10); // Lose 10 health per hit
        }
    }

    private void DecreaseHealth(int amount)
    {
        health -= amount;     // Reduce health
        CheckDeath();         // Check if player is dead

        PlayerLook.instance.AddShake(0.1f, 0.25f);
        UIManager.instance.InstantiateHitUI();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;  // Pause the game
    }
}
