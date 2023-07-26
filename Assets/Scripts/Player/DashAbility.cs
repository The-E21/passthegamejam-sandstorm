using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abilities/Dash Ability")]
public class DashAbility : Ability
{
    [SerializeField] private float distance;
    [SerializeField] private float time;
    [SerializeField] private bool intangibleWhileDashing;
    [SerializeField] private float dashDamage;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private GameObject dashVisual;
    [SerializeField] private string sound;

    private GameObject currentVisual;
    private Vector2 direction;
    private float speed;
    private float timer;
    private bool isDashing = false;

    private List<Collider2D> damaged;

    protected override void Function()
    {
        abilityHandler.playerMovement.enabled = false;
        if(intangibleWhileDashing){
            abilityHandler.playerIntangibility.makeIntangible();
        }
        speed = distance / time;
        timer = 0;
        isDashing = true;
        currentVisual = Instantiate(dashVisual, abilityHandler.transform);
        AudioManager.Instance.Play(sound);
        direction = Vector2.zero;
        damaged = new List<Collider2D>();
    }

    public override void Update()
    {
        base.Update();
        if(isDashing) {
            timer += Time.deltaTime;

            if(timer > time) {
                abilityHandler.playerMovement.enabled = true;
                isDashing = false;
                Destroy(currentVisual);
                abilityHandler.playerIntangibility.makeTangible();
            }

            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }

            if(direction == Vector2.zero){
                direction = Vector2.right;
            }

            if(dashDamage > 0) {
                DoDamge();
            }
        }
    }

    private void DoDamge() {
        foreach(Collider2D col in Physics2D.OverlapCircleAll(abilityHandler.transform.position, damageRadius, enemyLayers)){
            if(col.TryGetComponent(out Entity entity) && !damaged.Contains(col)){
                entity.Damage(dashDamage, abilityHandler.transform.position);
                damaged.Add(col);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate(); // Just in case it's not empty in the future
        if(isDashing)
            abilityHandler.rb.velocity = direction.normalized * speed;
    }
}
