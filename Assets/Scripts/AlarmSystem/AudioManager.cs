using System.Collections;
using UnityEngine;

namespace AlarmSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private Alarm _alarm;

        private AudioSource _audioSource;
        private Coroutine _activeCoroutine;

        private float _fillingRate = 0.4f;
        private float _requiredValue;

        private void Awake()
        {
            _audioSource = _alarm.GetComponent<AudioSource>();
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
            _requiredValue = 1f;

            _audioSource.Play();

            StartCoroutine(nameof(ChangeVolume));
        }

        private void StopSound()
        {
            _requiredValue = 0f;

            StopCoroutine(nameof(ChangeVolume));
            StartCoroutine(nameof(ChangeVolume));
        }

        private IEnumerator ChangeVolume()
        {
            while (_audioSource.volume != _requiredValue)
            {
                _audioSource.volume =
                    Mathf.MoveTowards(_audioSource.volume, _requiredValue, _fillingRate * Time.deltaTime);
                yield return null;
            }
        }
    }
}