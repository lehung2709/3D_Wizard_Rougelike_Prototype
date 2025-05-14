using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsAndImages : MonoBehaviour
{
    public static IconsAndImages Instance { get; private set; }

    [SerializeField] private Sprite burnedIcon;
    public Sprite BurnedIcon { get=>burnedIcon;  }

    [SerializeField] private Sprite stunnedIcon;
    public Sprite StunnedIcon { get=>stunnedIcon; }

    [SerializeField] private Sprite slowedDownIcon;
    public Sprite SlowedDownIcon { get=>slowedDownIcon;  }

    [SerializeField] private Sprite pBorder;
    public Sprite PBorder { get => pBorder; }

    [SerializeField] private Sprite cBorder;
    public Sprite CBorder { get => cBorder; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this); 
        }

        
    }

    

}
