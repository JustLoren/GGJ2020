using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tooltipper : MonoBehaviour
{
    #region Singleton bullshit
    public static Tooltipper Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Wtf, why are there two Tooltipper instances? Bad form, good sir.");
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    #endregion

    [System.Serializable]
    public class TooltipConfig
    {
        public TooltipType type;
        public GameObject parentObj;
        public TMPro.TextMeshProUGUI textObj;
        public List<GameObject> characters = new List<GameObject>();
    }

    public List<TooltipConfig> tooltips = new List<TooltipConfig>();

    public void ShowTip(TooltipType type, string message)
    {
        var tip = GetConfig(type);
        tip.textObj.text = message;
        tip.parentObj.SetActive(true);
    }

    public void HideTip(TooltipType type, string message)
    {
        var tip = GetConfig(type);

        if (tip.textObj.text == message)
            tip.parentObj.SetActive(false);
    }

    public void ShowSpeech(Character source, string message)
    {
        var tip = GetConfig(TooltipType.Speech);

        tip.textObj.text = message;
        tip.parentObj.SetActive(true);
        foreach(var c in tip.characters)
        {            
            c.SetActive(c.name == source.speechIconName);
        }
    }

    public void HideSpeech(Character source)
    {
        var tip = GetConfig(TooltipType.Speech);
        tip.parentObj.SetActive(false);
    }

    private TooltipConfig GetConfig(TooltipType type)
    {
        return tooltips.Single(t => t.type == type);
    }
}

public enum TooltipType
{
    Interactable,
    DesireSwap,
    Speech,
}

