using DevGio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private PlayerLoadout startLoadout;
    private Ability leftClickAbility;
    private Ability rightClickAbility;
    private Ability spaceAbility;

    public Vector2 aimVector => Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    public Vector2 aimNormalized => aimVector.normalized;
    public float aimAngle => aimNormalized.ToDegree();

    private Ability[] abilities;
    private PlayerEnergy energy;

    public PlayerEntity playerEntity {get; private set;}
    public PlayerMovement playerMovement {get; private set;}
    public Rigidbody2D rb {get; private set;}
    public PlayerIntangibility playerIntangibility {get; private set;}

    private void SetAbility(Ability ability, int id)
    {
        abilities[id] = Instantiate(ability);
        abilities[id].SetParent(this);
    }

    public void LoadPlayerLoadout(PlayerLoadout loadout) {
        GameObject oldSprites = GameObject.FindGameObjectWithTag("PlayerSprites");
        Instantiate(loadout.sprites, transform);
        SetAbility(loadout.leftClickAbility, 0);
        SetAbility(loadout.rightClickAbility, 1);
        SetAbility(loadout.spaceAbility, 2);

        playerEntity.maxHealth = loadout.health;
        playerEntity.health = 0;

        playerMovement.movementSpeed = loadout.speed;

        ExperianceManager.Instance.upgrades = loadout.upgrades;
        Destroy(oldSprites);
    }

    private void Awake()
    {
        energy = GetComponent<PlayerEnergy>();
        playerEntity = GetComponent<PlayerEntity>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerIntangibility = GetComponent<PlayerIntangibility>();

        abilities = new Ability[3];
    }

    private void Start() {
        LoadPlayerLoadout(startLoadout);
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            var ability = abilities[i];
            if (ability)
            {
                ability.Update();

                if (GetControl(i) && ability.isReady && energy.HasEnergy(ability.energyUsage) && !PauseHandler.Instance.paused)
                {
                    ability.Trigger();
                    energy.UseEnergy(ability.energyUsage);
                }
            }
        }
    }

    private void FixedUpdate() {
        for (int i = 0; i < abilities.Length; i++)
        {
            var ability = abilities[i];
            if (ability)
            {
                ability.FixedUpdate();
            }
        }
    }

    private bool GetControl(int id)
    {
        switch (id)
        {
            case 0:
                return Input.GetMouseButton(0);
            case 1:
                return Input.GetMouseButton(1);
            case 2:
                return Input.GetKey(KeyCode.Space);
            default:
                return false;
        }
    }
}
