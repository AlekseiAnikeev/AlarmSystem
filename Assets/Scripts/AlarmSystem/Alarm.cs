using System;
using UnityEngine;

namespace AlarmSystem
{
    public class Alarm : MonoBehaviour
    {
        public event Action MovementEnter;
        public event Action MovementLeave;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Thief _))
                MovementEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Thief _))
                MovementLeave?.Invoke();
        }
    }
}