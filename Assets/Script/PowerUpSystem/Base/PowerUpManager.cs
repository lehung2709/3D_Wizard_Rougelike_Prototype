using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PowerUpManager : MonoBehaviour
{
    PowerUpOption[] powerUpOptions;
    private PlayerStats playerStats; 
    private SpellsManager spellManager;
    private SpiritsManager spiritsManager;

    [SerializeField] private Transform pUOptionContainer;
    [SerializeField] private GameObject pUUI;

    [SerializeField] private StatPUOption StatPUOptionPrefab;
    [SerializeField] private SpellPUOption SpellPUOptionPrefab;
    [SerializeField] private SpiritPUOption SpiritPUOptionPrefab;

    private int XP=0;
    private int maxXP = 3;

    [SerializeField] private SliderAndFillBar XPBar;
    [SerializeField] private LayerMask XPLayer;
    [SerializeField] private float XPCollectionRadius;


    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        spellManager = GetComponent<SpellsManager>();
        spiritsManager = GetComponent<SpiritsManager>();
        
    }
    private void Start()
    {
        pUUI.SetActive(false);
        UpdateManaBar();
        
    }
    private void FixedUpdate()
    {
        ScanXP();
    }
    private void RandomPowerUpOption()
    {
        pUUI.SetActive(true);
        foreach (Transform child in pUOptionContainer)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0;i<4;i++) 
        {
            while (!Random()) { }

        }
        Time.timeScale = 0;
    } 
    
    private bool Random()
    {
        
        int randomIndex = UnityEngine.Random.Range(1,4);
        switch (randomIndex)
        {
            case 1:
                StatsPowerUpSO statsPowerUpSO= StatPowerUpLib.Instance.RandomPick();
                StatPUOption statPUOption= Instantiate(StatPUOptionPrefab);
                statPUOption.transform.SetParent(pUOptionContainer,false);
                statPUOption.SetData(statsPowerUpSO, playerStats);
                statPUOption.SetPUUI(pUUI);
                return true;
                
                
            case 2:
                SpellSO spellSO= SpellsLib.Instance.RandomPick();
                if(spellManager.IsFull(spellSO))return false;
                SpellPUOption spellPUOption= Instantiate(SpellPUOptionPrefab);
                spellPUOption.transform.SetParent (pUOptionContainer,false);
                spellPUOption.SetData(spellSO, spellManager);
                spellPUOption.SetPUUI(pUUI);

                return true;
                
                
                 
            case 3:
                SpiritSO spiritSO = SpiritsLib.Instance.RandomPick();
                if(spiritSO==null) return false;
                
                int index = spiritsManager.GetIndex(spiritSO);
                int level = 1;
                if (index < 0)
                {
                    if (spiritsManager.IsFull())
                    return false;
                    
                } 
                else
                {
                    level=spiritsManager.GetLevel(index);
                    if(level>3)return false;
                }    
                    
                SpiritPUOption spiritPUOption= Instantiate (SpiritPUOptionPrefab);
                spiritPUOption.transform.SetParent(pUOptionContainer, false);

                spiritPUOption.SetData(spiritSO, spiritsManager, index,level);
                spiritPUOption.SetPUUI(pUUI);

                return true;

            default:
                return false ;
        } 

    }

    public void CollectXP()
    {
        
            XP++;
            AudioManager.Instance.SpawnSoundEmitter(transform, "GainXP", Vector3.zero);

            if (XP == maxXP)
            {
                XP = 0;
                maxXP += 2;
                RandomPowerUpOption();

            }
            UpdateManaBar();

        
    }
    void ScanXP()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, XPCollectionRadius, XPLayer);

        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<XPCrystal>().StartBeingCollected(this.transform);
           
        }
    }

    public void UpdateManaBar()
    {
        XPBar.UpdateFiller(XP, maxXP);
    }



}
