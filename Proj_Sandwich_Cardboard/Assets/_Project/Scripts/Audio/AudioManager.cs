using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {

        [SerializeField] private string popSoundPath;
        [SerializeField] private string biteSoundPath;
        
        private const string AUDIO_PATH = "Sandwich/Sounds";

        private AudioClip[] _popSounds;
        private AudioClip[] _biteSounds;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _popSounds = Resources.LoadAll<AudioClip>(AUDIO_PATH + popSoundPath);
            _biteSounds = Resources.LoadAll<AudioClip>(AUDIO_PATH + biteSoundPath);
        }

        public void PlayPopSound()
        {
            int rand = Random.Range(0, _popSounds.Length);
            PlayClip(_popSounds[rand]);
        }
        
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
