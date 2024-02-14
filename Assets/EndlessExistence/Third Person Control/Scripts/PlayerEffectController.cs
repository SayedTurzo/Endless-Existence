using UnityEngine;

namespace EndlessExistence.Third_Person_Control.Scripts
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
