// HealthBarControl script
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    public RectTransform healthBarFillRect;  // Reference to the RectTransform component of the HealthBarFill object

    // Updated SetHealth method that accepts both current health and max health values
    public void SetHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = Mathf.Clamp01(currentHealth / maxHealth);  // Normalize the health value to a range of 0 to 1
        healthBarFillRect.sizeDelta = new Vector2(normalizedHealth * 100f, healthBarFillRect.sizeDelta.y);  // Update the width of the health bar fill
    }
}