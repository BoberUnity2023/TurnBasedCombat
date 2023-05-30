using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Data/Stats/Stats")]
public class StatsData : ScriptableObject
{
    [SerializeField] private float _strength;
    [SerializeField] private float _dexterity;
    [SerializeField] private float _durability;
    [SerializeField] private float _brain;
    [SerializeField] private float _luck;

    public float Strength => _strength;
    public float Dexterity => _dexterity;
    public float Durability => _durability;
    public float Brain => _brain;
    public float Luck => _luck;
}
