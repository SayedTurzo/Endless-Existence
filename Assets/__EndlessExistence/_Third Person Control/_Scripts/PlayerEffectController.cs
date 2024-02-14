using UnityEngine;

namespace __EndlessExistence._Third_Person_Control._Scripts
{
    public class PlayerEffectController : MonoBehaviour
    {
        public GameObject effectHolder;

        public void PlayEffect(ParticleSystem effect)
        {
            ParticleSystem currentEffect = Instantiate(effect, effectHolder.transform);
            currentEffect.Play();
        }
    }
}
