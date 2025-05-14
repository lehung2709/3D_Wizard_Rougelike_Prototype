using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnapToCenter : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private RectTransform sampleListItem;
    [SerializeField] private HorizontalLayoutGroup HLG;
    [SerializeField] private TMP_Text nameLabel;            
               

    private bool isSnapped= false;     
    //private int currentItemIndex = 0;     
    private float snapSpeed;
    [SerializeField] private float snapForce;



    void Update()
    {
        int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x) / (sampleListItem.rect.width + HLG.spacing));

        if (scrollRect.velocity.magnitude < 200 && !isSnapped)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z
            );

            if (contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))
            {
                isSnapped = true;
            }
        }

        if (scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }
    }

}
