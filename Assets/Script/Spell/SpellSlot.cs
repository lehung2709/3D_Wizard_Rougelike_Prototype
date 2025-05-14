using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected SpellsManager spellsManager;

    
    protected Image avatar;
    protected Image border;
    
    protected virtual void Awake()
    {
        avatar = GetComponent<Image>();
        border= transform.Find("Border").GetComponent<Image>();
    }
    
   

    public void SetSpellManager(SpellsManager spellsManager)
    {
        this.spellsManager = spellsManager;
    }

 
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
        spellsManager.Select(this);
        AudioManager.Instance.PlayBtnSound();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public virtual void SetData(SpellSO spellSO)
    {
        gameObject.SetActive(true);
        
    }   
    public virtual void Cast(Player player)
    {

    } 
    public virtual SpellSO GetSpellSO()
    {
        return null;
    }

    public virtual bool IsEmpty()
    {
        return true;  
    }  
    
    public void SetBorder(Sprite image)
    {
        border.sprite = image;
    }    
    

    
}
