using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//This script needs a particle system
[RequireComponent(typeof(ParticleSystem))]
public class SoundSystem : MonoBehaviour
{
    //This script will play audio when the particles die and are born to make the swoosh and boom!
    #region Variables
    [SerializeField] private ParticleSystem fireworksPS; //Firework particle system
    public AudioSource bornPartcilesAudios; //beginning of swoosh to make boom!
    public AudioSource dieParticleAudios; //end of swoosh
    private int numberOfParticles = 0; //number of particles 
    #endregion

    // Update is called once per frame
    void Update()
    {
        //Gives absolute value between the number of particles and the particle count in the fireworks
        int amount = Mathf.Abs(numberOfParticles - fireworksPS.particleCount);

        //If particles in fireworks is less than number of particles
        if(fireworksPS.particleCount < numberOfParticles)
        {
            //Start Coroutine of boom particles audio
            StartCoroutine(PlayFireworks(bornPartcilesAudios, amount));
        }
        else
        {
            //Start Coroutine of swoosh particles audio
            StartCoroutine(PlayFireworks(dieParticleAudios, amount));
        }

        //Reset particle count by making them equal to each other
        numberOfParticles = fireworksPS.particleCount;
    }

    private IEnumerator PlayFireworks(AudioSource _clip, int amount)
    {
        _clip.Play();
        yield return null;
    }
}
