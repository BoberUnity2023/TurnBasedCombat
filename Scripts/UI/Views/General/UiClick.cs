using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DNVMVC.Views
{
    public class UiClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;

        public async void OnPointerClick(PointerEventData eventData)
        {
            await _image.transform.DOScale(Vector3.one * 0.5f, 0.25f).SetEase(Ease.Linear).AsyncWaitForCompletion();
            await _image.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }
    }
}