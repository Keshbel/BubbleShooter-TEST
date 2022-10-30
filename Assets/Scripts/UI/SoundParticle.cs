using UnityEngine;

public class SoundParticle : MonoBehaviour
{

    public void Stop()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().swish[0]);
    }
    public void Hit()
    {
        SoundBase.GetInstance().GetComponent<AudioSource>().PlayOneShot(SoundBase.GetInstance().hit);
    }

}
