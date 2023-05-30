using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DNVMVC.Views
{
    [System.Serializable]
    public class AbilityView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private TextMeshProUGUI _name;


        public void Repaint(AbilityModel abilityModel)
        {
            var name = abilityModel.Ability.ToString();
            var index = name.IndexOf('.') + 1;
            var newName = name.Substring(index);

            _name.text = $"{newName}";

            _icon.color = Random.ColorHSV();
        }

        public async void SelectButton()
        {
            await _icon.transform.DOScale(Vector3.one * 0.5f, 0.125f).SetEase(Ease.Linear).AsyncWaitForCompletion();
            await _icon.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }
    }
}