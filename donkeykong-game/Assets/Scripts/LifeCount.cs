using UnityEngine;
using UnityEngine.UI;


public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;


    public void LoseLife()
    {
        //do nothing if out of lives
        if (livesRemaining == 0)
        {
            return;
        }
        //Decrease value of lives remaining
        livesRemaining--;
        //Hide one of the life images
        lives[livesRemaining].enabled = false;


        //play the getting hurt audio
        AudioManager.instance.PlaySFX("hurt");


        //Lose when you run out of lives
        if (livesRemaining == 0)
        {
            FindObjectOfType<Player>().Die();
        }
    }


}
