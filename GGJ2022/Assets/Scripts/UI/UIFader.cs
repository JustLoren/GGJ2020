using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFader : MonoBehaviour
{
    #region Singleton shit
    public static UIFader Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("Why is there another UI Fader? Gross.");
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    #endregion

    public Image curtain;
    public Image credits;
    public float fadeTime = .5f;
    private float currentAlpha = 1f;
    private float targetAlpha = 1f;    

    private void Start()
    {
        //HideCurtain();
    }

    public void ShowCredits(StarterAssets.StarterAssetsInputs inputToWatch)
    {        
        ShowCurtain();
        StartCoroutine(WaitForExit(inputToWatch));
    }

    private IEnumerator WaitForExit(StarterAssets.StarterAssetsInputs input)
    {
        credits.gameObject.SetActive(true);

        yield return new WaitForSeconds(fadeTime);

        while (!input.interact)
            yield return null;
                
        Mirror.NetworkManager.singleton.StopHost();
    }

    public void ShowCurtain()
    {
        this.targetAlpha = 1f;
        this.enabled = true;
        curtain.enabled = true;
    }

    public void HideCurtain()
    {
        this.targetAlpha = 0f;
        this.enabled = true;
    }

    private void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeTime * Time.deltaTime);
        curtain.color = new Color(0, 0, 0, currentAlpha);
        credits.color = new Color(1, 1, 1, currentAlpha);
        if (Mathf.Approximately(currentAlpha, targetAlpha))
        {
            this.enabled = false;
            if (Mathf.Approximately(targetAlpha, 0f))
            {
                curtain.enabled = false;
            }
        }
    }
}
