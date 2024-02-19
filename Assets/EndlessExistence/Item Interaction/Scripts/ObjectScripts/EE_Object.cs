using CustomInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace EndlessExistence.Item_Interaction.Scripts.ObjectScripts
{
    public class EE_Object : MonoBehaviour
    {
        [HorizontalLine("Item Component", 2, FixedColor.Red)] 
        [SerializeField] private GameObject interactCanvas;
        [FormerlySerializedAs("itemDescriptionPanel")] [SerializeField] public GameObject objectDescriptionPanel;
        [SerializeField] public GameObject objectInteractionPanel;
        [SerializeField] public GameObject objectInspectPanel;

        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] public TextMeshProUGUI objectDescription;
        [SerializeField] public TextMeshProUGUI inspectText;
    
        [MessageBox("Default tag for Player is 'Player'.Change if needed!",MessageBoxType.Info)]
        [CustomInspector.Tag] [SerializeField] internal string playerTag = "Player";

        [SerializeField] internal string interactTextString = "E";
        [SerializeField] internal string inspectTextString = "I";
    
        [HorizontalLine("Effect", 3, FixedColor.Purple)] 
        [MessageBox("To use effect drag and drop your effect into the EffectHolder gameobject of Item in hierarchy",MessageBoxType.Info)]
        [SerializeField] private GameObject effectHolder;
        [SerializeField] private bool useEffect = false;
        [ShowIf(nameof(useEffect))]
        [SerializeField]private GameObject effect;
        private GameObject _effect;
        
        [HorizontalLine("",5,FixedColor.Red)] 
        [BackgroundColor(FixedColor.DustyBlue)]
        [SerializeField,ReadOnly]
        [TextArea(minLines: 1, maxLines: 30)]
        private string scriptInfo =   "  Please add required component for interacting with the item." +
                                      "\nExample : If you want health item then add EE_HealthItem component to this object." +
                                      "\nBy default we have Health, Stamina and Defence Item." +
                                      "\nIf you want your custom item then just create a new class inheriting ItemContainer" +
                                      "\nFor example check the EE_DefaultItem.cs class";
        
        
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

            interactText.text = interactTextString;
            inspectText.text = inspectTextString;
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
}
