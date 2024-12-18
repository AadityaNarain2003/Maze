using UnityEngine;
using System.Collections;

public class CoinAnimationHandler : MonoBehaviour
{
    // This coroutine will wait for the next frame and then start the animation
    public IEnumerator StartCoinAnimation(GameObject coin)
    {
        // Wait until the next frame
        yield return null;  

        // Get the animator and trigger the animation
        Animator animator = coin.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind(); // Reset to default state
            animator.Update(0); // Apply the first frame immediately
            animator.Play("Coin_Animation");  // Replace with your animation state name
        }
    }
}
