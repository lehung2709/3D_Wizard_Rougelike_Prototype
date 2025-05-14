using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsLib : MonoBehaviour
{
    public static SpiritsLib Instance {  get; private set; }

    [SerializeField] private List<SpiritSO> spirits;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Destroy(this);
        }    
       
            
    }
    public SpiritSO RandomPick()
    {
        if (spirits == null || spirits.Count == 0)
        {
            return null; 
        }

        int randomIndex = Random.Range(0, spirits.Count);

        return spirits[randomIndex];

    }    
}
