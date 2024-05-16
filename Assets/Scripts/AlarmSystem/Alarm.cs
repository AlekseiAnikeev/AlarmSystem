using System;
using UnityEngine;

namespace AlarmSystem
{
    public class Alarm : MonoBehaviour
    {
        public event Action<bool> MovementEnter;
        public event Action<bool> MovementLeave;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Thief _))
                MovementEnter?.Invoke(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Thief _))
                MovementLeave?.Invoke(false);
        }
    }
}