using System;
using UnityEngine;

namespace AI
{
    public class BattleMarker : MonoBehaviour
    {
        [SerializeField] private GameObject _marker;

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            _marker.SetActive(true);
        }

        public void Hide()
        {
            _marker.SetActive(false);
        }
    }
}