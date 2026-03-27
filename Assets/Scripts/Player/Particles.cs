using UnityEngine;

public partial class Player : MonoBehaviour
{
    public ParticleSystem dustParticles;

    public float dustDelay = .2f;

    private float timer;

    void UpdateParticles ()
    {
        if(rb.linearVelocity.magnitude > 0.1f)
        {
            timer += Time.deltaTime;

            if (timer >= dustDelay)
            {
                CreateDust();
                timer = 0;
            }
        } else
        {
            timer = dustDelay;
        }
    }

    void CreateDust ()
    {
        dustParticles.Emit(3);
    }
}
