using System.Collections;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float roundDuration = 60.0f;
    public float remainingTime = 0f;
    private bool roundEnded = true;
    private bool roundStarted = false;
    public const float roundCountdown = 3.0f;


    public void StartGameTimer()
    {
        initRoundCountdown();
    }

    public void SetRoundDuration(float time)
    {
        roundDuration = time;
    }

    void resetTimer ()
    {
        remainingTime = roundDuration;
        roundEnded = false;
        StartCoroutine(RoundTimer());
    }

    void CountDown()
    {
        if (roundEnded)
        {
            return;
        }

        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
        } else if(remainingTime <= 0f )
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        Debug.Log("Timer Ended");
        roundEnded = true;
    }
    void initRoundCountdown()
    {
        remainingTime = roundCountdown;
        StartCoroutine(RoundCountdown());
    }

    IEnumerator RoundCountdown()
    {
        while (!roundStarted)
        {
            yield return new WaitForSeconds(1f);
            if(remainingTime > 0f)
            {
                remainingTime -= 1f;
            } else
            {
                remainingTime = 0f;
                roundStarted = true;
                resetTimer();
            }
        }   
    }

    IEnumerator RoundTimer()
    {
        while (!roundEnded)
        {
            yield return new WaitForSeconds(1f);
            if (remainingTime > 0f)
            {
                remainingTime -= 1f;
            } else
            {
                remainingTime = 0f;
                roundEnded = true;
                timerEnded();
            }
        }
    }

}
