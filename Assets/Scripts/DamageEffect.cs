using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    public Image damageOverlay; // Assign the UI Panel Image here
    public float fadeDuration = 1f; // How long the effect lasts

    private void Start()
    {
        if (damageOverlay)
            damageOverlay.color = new Color(1, 0, 0, 0); // Ensure it starts invisible
    }

    public void TriggerDamageEffect(Color effectColor)
    {
        StartCoroutine(FadeDamageEffect(effectColor));
    }

    private IEnumerator FadeDamageEffect(Color effectColor)
    {
        float elapsedTime = 0f;
        Color startColor = new Color(effectColor.r, effectColor.g, effectColor.b, 0.5f); // 50% opacity
        Color endColor = new Color(effectColor.r, effectColor.g, effectColor.b, 0); // Fully transparent

        damageOverlay.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            damageOverlay.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        damageOverlay.color = endColor; // Ensure it fully fades out
    }
}
