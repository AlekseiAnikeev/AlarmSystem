using System;
using System.Collections;
using UnityEngine;

namespace AlarmSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class TriggerVolume : MonoBehaviour
    {
        [SerializeField] private Alarm _alarm;

        private AudioSource _audioSource;
        private Coroutine _activeCoroutine;

        private float _fillingRate = 0.4f;
        private float _maxVolume = 1f;
        private float _minVolume = 0f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0;
        }

        private void OnEnable()
        {
            _alarm.MovementEnter += PlaySound;
            _alarm.MovementLeave += StopSound;
        }

        private void OnDisable()
        {
            _alarm.MovementEnter -= PlaySound;
            _alarm.MovementLeave -= StopSound;
        }

        private void PlaySound()
        {
            _activeCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
        }

        private void StopSound()
        {
            if (_activeCoroutine != null)
                StopCoroutine(_activeCoroutine);

            _activeCoroutine = StartCoroutine(ChangeVolume(_minVolume));
        }

        private IEnumerator ChangeVolume(float targetVolume)
        {
            float accuracy = 0.00001f;

            if (Math.Abs(targetVolume - _maxVolume) < accuracy)
                _audioSource.Play();

            while (Math.Abs(_audioSource.volume - targetVolume) > accuracy)
            {
                _audioSource.volume =
                    Mathf.MoveTowards(_audioSource.volume, targetVolume, _fillingRate * Time.deltaTime);

                yield return null;
            }
            
            if (Math.Abs(_audioSource.volume - _minVolume) < accuracy)
                _audioSource.Stop();
        }
    }
}