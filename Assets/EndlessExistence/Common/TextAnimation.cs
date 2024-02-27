using DG.Tweening;
using TMPro;
using UnityEngine;

namespace EndlessExistence.Common
{
    public static class TextAnimation
    {
        // Move up and fade out the TextMeshPro text simultaneously
        public static void MoveAndFadeText(TextMeshProUGUI textMeshPro,Vector3 initPos, Vector3 targetPosition, float moveDuration, float fadeDuration)
        {
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference is null. Please provide a valid TextMeshPro component.");
                return;
            }

            // Set the initial position
            Vector3 initialPosition = initPos;

            // Tween the anchored position using DOTween
            textMeshPro.rectTransform.DOAnchorPosY(targetPosition.y, moveDuration)
                .SetEase(Ease.OutQuad)
                .OnUpdate(() =>
                {
                    // Calculate the normalized progress of the move animation
                    float normalizedProgress = textMeshPro.rectTransform.anchoredPosition.y / targetPosition.y;

                    // Fade out the TextMeshPro text based on the normalized progress
                    //textMeshPro.DOFade(1f - normalizedProgress, fadeDuration);
                })
                .OnComplete(() =>
                {
                    //Debug.Log("TextMeshPro move, fade, and move back animation completed");
                    // Instantly move the TextMeshPro text back to its initial position
                    InstantMoveText(textMeshPro, initialPosition);
                });
        }
        
        // Instantly move the TextMeshPro text
        private static void InstantMoveText(TextMeshProUGUI textMeshPro, Vector3 targetPosition)
        {
            textMeshPro.rectTransform.anchoredPosition = targetPosition;
            textMeshPro.gameObject.SetActive(false);
            
            //Debug.Log("TextMeshPro instantly moved to position: " + targetPosition);
        }
    }
}
