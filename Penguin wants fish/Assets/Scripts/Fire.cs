using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem particleEffect;

    

    public void PlayParticles()
    {
        if (particleEffect != null && !particleEffect.isPlaying)
        {
            particleEffect.Play();
        }
    }

    public void StopParticles()
    {
        if (particleEffect != null && particleEffect.isPlaying)
        {
            particleEffect.Stop();
        }
    }
}
