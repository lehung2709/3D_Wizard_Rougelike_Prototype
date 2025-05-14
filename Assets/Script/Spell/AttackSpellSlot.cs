using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackSpellSlot : SpellSlot
{
    private float holdTime = 1.0f;
    private float currentHoldTime = 0f;
    private bool isHolding = false;
    [SerializeField]private AttackSpellSO attackSpellSO;

    protected override void Awake()
    {
        base.Awake();
        if (attackSpellSO != null )
        {
            avatar.sprite= attackSpellSO.avatar;
        }
    }

   private  void Start()
    {
       if( attackSpellSO == null )gameObject.SetActive( false );
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isHolding = true;
        currentHoldTime = 0f;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isHolding = false;
        currentHoldTime = 0f;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= holdTime)
            {
                spellsManager.ShowOptionBtn();
            }
        }
    }

    public override void Cast(Player player)
    {
        if (!player.stats.IsNotStunned) return;
        if (player.stats.Mana < attackSpellSO.manaCost) return;

        player.animator.SetTrigger("Casting");
        player.stats.Mana -= attackSpellSO.manaCost;
        player.stats.UpdateManaBar();

        StartCoroutine(Shoot(player.shootTimeHolder,player.shootPoint,player.stats.Damage,player.transform.forward,player.enemyMask,player.stats.ProjectilesPerCast));
        
    }
    private IEnumerator Shoot(float shootTimeHolder,Transform shootPoint,float damage,Vector3 direct,LayerMask enemyMask,int count)
    {
        yield return new WaitForSeconds(shootTimeHolder);

        AudioManager.Instance.SpawnSoundEmitter(null, "CastSpell", Vector3.zero);

        for (int i = 0; i < count; i++)
        {
            BaseProjectile pro = Instantiate(attackSpellSO.projectile, shootPoint.position, Quaternion.identity);

            pro.SetData(attackSpellSO.vel, direct, damage * attackSpellSO.damageScalingFactor, attackSpellSO.effects, attackSpellSO.damageRangeRadius, enemyMask);
            yield return new WaitForSeconds(0.1f);
        }

    }

    public override SpellSO GetSpellSO()
    {
        return attackSpellSO;
    }

    public override void SetData(SpellSO spellSO)
    {
        base.SetData(spellSO);
        gameObject.SetActive(true);
        attackSpellSO = (AttackSpellSO)spellSO;
        avatar.sprite = attackSpellSO.avatar;
        
    }
    public override bool IsEmpty()
    {
        return (attackSpellSO == null);
    }
}
