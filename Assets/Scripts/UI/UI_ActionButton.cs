using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ActionButton : MonoBehaviour
{
    [Header("Skill Reference")]
    [HideInInspector] public Skill actionSkill;
    public UI_ActionPanelBehaviour actionPanel;

    [Header("Prefab References")]
    public GameObject paCostPrefab;
    float costPrefabHeightSize = 0;

    [Header("IMAGE References")]
    public Image actionImage;
    public Image backgroundImage;
    List<Image> costPoints;

    [Header("RECT References")]
    public RectTransform rect;
    public RectTransform costParent;

    [Header("TMP References")]
    public TextMeshProUGUI tooltipDescription;
    public TextMeshProUGUI tooltipName;

    [Header("Tooltip Animation")]
    public Animator animator;

    public AnimationCurve tooltipCurve;
    public float animTime = 0;
    private float currentTime = 0;

    bool isUnfold = true;
    public bool isSelected = false;
    bool isMoving = false;

    private float current;
    private float diff;

    public float unfoldPos = 300f;



    void Awake()
    {
        costPrefabHeightSize = paCostPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    void Update()
    {
        if (isMoving)
            TooltipAnimation();
    }


    public void Highlighting()
    {
        if (!isSelected)
            animator.Play("Highlighted");
    }

    public void Clicking()
    {
        animator.Play("Clicking");
    }

    public void BackToNormal()
    {
        if (!isSelected)
            animator.Play("Normal");
    }

    /// <summary>
    /// Assign skill values to its corresponding button
    /// </summary>
    /// <param name="skill">Button skill</param>
    public void SetUpTooltip()
    {
        this.gameObject.name = actionSkill.skillName;
        tooltipName.text = actionSkill.skillName;
        tooltipDescription.text = actionSkill.description;
    }

    public void CheckAndRefreshActionUI(int currentPA)
    {
        CheckGunShotException();
        CheckPlayerPA(currentPA);
    }

    public void ShowTooltip()
    {
        if (!isUnfold)
            return;

        isMoving = false;

        current = rect.anchoredPosition3D.x;
        diff = unfoldPos - current;

        isUnfold = false;
        isMoving = true;
    }

    public void HideTooltip()
    {
        if (isUnfold)
            return;

        isMoving = false;

        current = rect.anchoredPosition3D.x;
        diff = 0 - current;

        isUnfold = true;
        isMoving = true;
    }

    public void SetUpActionPointsDisplay()
    {

        float newPos = (((costPrefabHeightSize * 0.5f) * (actionSkill.cost - 1)) * -1);
        costPoints = new List<Image>();

        for (int i = 0; i < actionSkill.cost; i++)
        {
            GameObject costObj = Instantiate(paCostPrefab, Vector3.zero, Quaternion.identity, costParent.transform);
            RectTransform costRect = costObj.GetComponent<RectTransform>();
            Image costImage = costObj.GetComponent<Image>();


            costRect.anchoredPosition3D = new Vector3(0, costPrefabHeightSize * i, 0);
            costPoints.Add(costImage);
        }

        if ((actionSkill.cost - 1) > -1)
            costParent.anchoredPosition3D = new Vector3(costParent.anchoredPosition3D.x, newPos, costParent.anchoredPosition3D.z);
    }

    public void PreviewSkillAction()
    {
        if (actionPanel.selectedAction != null && actionPanel.selectedAction != this)
        {
            actionPanel.selectedAction.isSelected = false;
            actionPanel.selectedAction.PreviewSkillAction();
        }

        isSelected = !isSelected;

        if (!isSelected)
            actionPanel.selectedAction = null;
        else
            actionPanel.selectedAction = this;

        actionSkill.Preview(PlayerManager.instance.playerCharacter);
    }

    private void CheckSkillCondition()
    {
        if (actionSkill.HasAvailableTarget(PlayerManager.instance.playerCharacter) == null)
            return;

        if (actionSkill.HasAvailableTarget(PlayerManager.instance.playerCharacter).Count == 0)
        {
            actionImage.sprite = actionSkill.unenabledSprite;
        }
    }

    public void CheckGunShotException()
    {
        if (actionSkill is GunShot || actionSkill is Reload)
        {
            if (!PlayerManager.instance.playerCharacter.isGunLoaded)
            {
                actionSkill = PlayerManager.instance.playerCharacter.reloadSkill;
            }
            else
            {
                actionSkill = PlayerManager.instance.playerCharacter.gunShotSkill;
            }

            SetUpTooltip();
        }
        else
        {
            SetUpTooltip();
        }
    }

    private void CheckPlayerPA(int currentPACompared)
    {
        if (currentPACompared < actionSkill.cost)
        {
            for (int i = 0; i < currentPACompared; i++)
            {
                costPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPA;
            }

            for (int i = currentPACompared; i < actionSkill.cost; i++)
            {
                costPoints[i].sprite = UI_Manager.instance.uiPreset.usedPA;
            }

            actionImage.sprite = actionSkill.unenabledSprite;
        }
        else
        {
            for (int i = 0; i < costPoints.Count; i++)
            {
                costPoints[i].sprite = UI_Manager.instance.uiPreset.unusedPA;
            }

            actionImage.sprite = actionSkill.enabledSprite;
        }

        CheckSkillCondition();
    }

    private void TooltipAnimation()
    {
        if (isMoving)
        {
            if (isUnfold)
            {
                if (currentTime > 0)
                {
                    currentTime -= Time.deltaTime;

                    float percent = tooltipCurve.Evaluate(currentTime / animTime);

                    rect.anchoredPosition3D = new Vector3((0 - (diff * percent)), rect.anchoredPosition3D.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    isMoving = false;
                    currentTime = 0;
                }
            }
            else
            {
                if (currentTime < animTime)
                {
                    currentTime += Time.deltaTime;

                    float percent = tooltipCurve.Evaluate(currentTime / animTime);

                    rect.anchoredPosition3D = new Vector3(current + (diff * percent), rect.anchoredPosition3D.y, rect.anchoredPosition3D.z);
                }
                else
                {
                    isMoving = false;
                    currentTime = animTime;
                }
            }
        }
    }
}
