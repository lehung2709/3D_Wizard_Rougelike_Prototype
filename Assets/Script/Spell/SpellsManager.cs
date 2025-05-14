using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsManager : MonoBehaviour
{
    
    [SerializeField]private List<AttackSpellSlot> attackSpellSlots;
    public SpellSlot SelectedSpell { get;private set; }
    [SerializeField]private SpellSlot defaultSpell;
    [SerializeField] private GameObject mergeBtn;
    [SerializeField] private GameObject dropBtn;
    [SerializeField] private GameObject cancelMergeBtn;

    [SerializeField] private GameObject arrowAndBtn;
    private bool isInMergeMode=false;

    
    
    private void Awake()
    {
        Select(defaultSpell);
        defaultSpell.SetSpellManager(this);
       
        foreach(SpellSlot option in attackSpellSlots)
        {
            option.SetSpellManager(this);
        }    
    }
    
    public void Select(SpellSlot option)
    {
        
        if (isInMergeMode)
        {
            Merge(option);
        }
        else
        {

            SelectedSpell = option;
            arrowAndBtn.transform.position = option.transform.position;
        }
        
    }    

    public void ShowOptionBtn()
    {
        if(SelectedSpell==defaultSpell)return;
        if(SpellsLib.Instance.primitiveAttackSpells.IndexOf((AttackSpellSO)SelectedSpell.GetSpellSO())!=-1) mergeBtn.gameObject.SetActive(true);
        dropBtn.SetActive(true);
        

    }
    public void HideOptionBtn()
    {
        mergeBtn.SetActive(false);
        dropBtn.SetActive(false);
        cancelMergeBtn.SetActive(false);

    }
    public void EnterMergeMode()
    {
        isInMergeMode = true;
        defaultSpell.gameObject.SetActive(false);
        HideOptionBtn();
        cancelMergeBtn.SetActive(true);
    }   
    
    private void Merge(SpellSlot option)
    {
        if (option == SelectedSpell) return;
        string key = SelectedSpell.GetSpellSO().spellName + "_" + option.GetSpellSO().spellName;
        if (SpellsLib.Instance.attackSpells.ContainsKey(key))
        {
            SelectedSpell.SetData(SpellsLib.Instance.attackSpells[key]);
            option.gameObject.SetActive(false);
        }
        else
        {
            key = option.GetSpellSO().spellName + "_" + SelectedSpell.GetSpellSO().spellName;
            SelectedSpell.SetData(SpellsLib.Instance.attackSpells[key]);
            option.gameObject.SetActive(false);
        }
        
        SelectedSpell.SetBorder(IconsAndImages.Instance.CBorder);
        arrowAndBtn.transform.position = SelectedSpell.transform.position;


        ExitMergeMode();
    }    

    public void Drop()
    {
        SelectedSpell.gameObject.SetActive(false);
        Select(defaultSpell);
        HideOptionBtn();
    }    
    public void ExitMergeMode()
    {
        isInMergeMode = false;
        defaultSpell.gameObject.SetActive(true);
        cancelMergeBtn.SetActive(false );
    }   

    public bool IsFull(SpellSO spellSO)
    {
        if (spellSO is AttackSpellSO)
        {
            foreach (AttackSpellSlot attackSpellSlot in attackSpellSlots)
            {
                if (attackSpellSlot.IsEmpty() || attackSpellSlot.GetSpellSO()==spellSO)
                {
                    return false;
                }
            }
        }
        return true;

    }

    public void TakeSpell(SpellSO spellSO)
    {
        if(spellSO is AttackSpellSO)
        {
            foreach (AttackSpellSlot attackSpellSlot in attackSpellSlots)
            {
                if (attackSpellSlot.IsEmpty())
                {
                    attackSpellSlot.SetData(spellSO);
                    attackSpellSlot.SetBorder(IconsAndImages.Instance.PBorder);
                    return;
                }
            }
        } 
            
       
    }    


    
    

    

}
