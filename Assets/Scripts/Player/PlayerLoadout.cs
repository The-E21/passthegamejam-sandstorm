using UnityEngine;

[CreateAssetMenu(menuName = "PlayerLoadout")]
public class PlayerLoadout : ScriptableObject
{
    public GameObject sprites;
    
    public Ability leftClickAbility;
    public Ability rightClickAbility;
    public Ability spaceAbility;

    public float health;
    public float speed;

    public PlayerLoadout[] upgrades;
}
