using Components.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DNVMVC.Views
{
    public class AbilityPresenter : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private AbilityModel _abilityModel;
        [SerializeField] private AbilityView _abilityView;
        private Player _player;

        public void SetPlayer(Player player)
        {
            _player = player;
        }
        public void UpdateAbilityModel(AbilityModel abilityModel)
        {
            _abilityModel = abilityModel;
            _abilityView.Repaint(_abilityModel);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            // _player.SystemUsingAbilities.TrySetActiveAbility(_abilityModel.Ability);
            _abilityView.SelectButton();
        }
    }
}