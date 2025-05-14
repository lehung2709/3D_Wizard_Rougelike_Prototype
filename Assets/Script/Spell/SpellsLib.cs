using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsLib : MonoBehaviour
{

    [System.Serializable]
    private class CombinationAttackSpell
    {
        public string key;//vd:fire_water
        public AttackSpellSO SpellSO;
    }
    public static SpellsLib Instance {  get; private set; }
    [SerializeField] public List<AttackSpellSO> primitiveAttackSpells;
    [SerializeField] private List<CombinationAttackSpell> combinationAttackSpells;

    public Dictionary<string, AttackSpellSO> attackSpells;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        attackSpells = new Dictionary<string, AttackSpellSO>();
        foreach (AttackSpellSO spell in primitiveAttackSpells)
        {
            attackSpells.Add(spell.name, spell);
        }
        foreach (CombinationAttackSpell comb in combinationAttackSpells)
        {
            attackSpells.Add(comb.key, comb.SpellSO);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateCombinationSpells()
    {

        combinationAttackSpells.Clear();

        for (int i = 0; i < primitiveAttackSpells.Count; i++)
        {
            for (int j = i + 1; j < primitiveAttackSpells.Count; j++)
            {
                string combCode = primitiveAttackSpells[i].spellName + "_" + primitiveAttackSpells[j].spellName;
                CombinationAttackSpell newCombination = new CombinationAttackSpell
                {
                    key = combCode,

                };
                combinationAttackSpells.Add(newCombination);
            }
        }
    }


    private void OnValidate()
    {
        //GenerateCombinationSpells();
    }

    public SpellSO RandomPick()
    {
        if (primitiveAttackSpells == null || primitiveAttackSpells.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, primitiveAttackSpells.Count);

        return primitiveAttackSpells[randomIndex];

    }
}
