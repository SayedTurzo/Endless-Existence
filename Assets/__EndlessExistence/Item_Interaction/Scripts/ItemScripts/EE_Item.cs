using System;
using UnityEngine;
using CustomInspector;
using TMPro;
using UnityEngine.Serialization;

public class EE_Item : MonoBehaviour
{
    [HorizontalLine("Item Component", 2, FixedColor.Red)] 
    [SerializeField] private GameObject interactCanvas;

    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    
    [MessageBox("Default tag for Player is 'Player'.Change if needed!",MessageBoxType.Info)]
    [SerializeField] internal string playerTag = "Player";
    
    [HorizontalLine("Effect", 2, FixedColor.Purple)] 
    [MessageBox("To use effect drag and drop your effect into the EffectHolder gameobject of Item in hierarchy",MessageBoxType.Info)]
    [SerializeField] private GameObject effectHolder;
    [SerializeField] private bool useEffect = false;
    [ShowIf(nameof(useEffect))][SerializeField] private GameObject effect;

    private GameObject _effect;

    private void Awake()
    {
        if (useEffect && effect!=null && effectHolder!=null)
        {
            _effect = Instantiate(effect,effectHolder.transform);
            _effect.SetActive(false);
        }
        else if(useEffect && effect==null || effectHolder ==null)
        {
            Debug.Log("Please assign the EffectHolder and the Effect if you want to use effect");
        }

        if (interactCanvas != null)
        {
            interactCanvas.SetActive(false);
        }
    }

    internal void TriggerCanvas()
    {
        if (interactCanvas!=null)
        {
            interactCanvas.SetActive(!interactCanvas.activeSelf);
        }
    }

    internal void TriggerEffect()
    {
        if (useEffect && effectHolder!=null && effect!=null)
        {
            _effect.SetActive(!_effect.activeSelf);
        }
    }
}