using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    //Encapsulates logic for retreating and playing multiple sounds
    public class AudioManager : MonoBehaviour
    {
        //constant path
        private const string AUDIO_PATH = "Sandwich/Sounds";

        //private members
        private AudioClip[] _popSounds;
        private AudioClip[] _biteSounds;
        private AudioSource _audioSource;
        
        //exposed fields
        [SerializeField] private string popSoundPath;
        [SerializeField] private string biteSoundPath;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _popSounds = Resources.LoadAll<AudioClip>(AUDIO_PATH + popSoundPath);
            _biteSounds = Resources.LoadAll<AudioClip>(AUDIO_PATH + biteSoundPath);
        }

        //picks a random pop sound to play
        public void PlayPopSound()
        {
            int rand = Random.Range(0, _popSounds.Length);
            PlayClip(_popSounds[rand]);
        }
        
        //picks a random bite sound to play
        public void PlayBiteSound()
        {
            int rand = Random.Range(0, _biteSounds.Length);
            PlayClip(_biteSounds[rand]);
        }
        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
