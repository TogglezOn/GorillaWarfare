using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NeoFPS
{
    public class NeoFpsTouchVirtualAnalog : BaseTouchControl
    {
        [Header("Axes")]

        [SerializeField, Tooltip("The axis to apply the virtual analog stick's horizontal axis to.")]
        private FpsInputAxis m_HorizontalAxis = FpsInputAxis.MoveX;
        [SerializeField, Tooltip("The axis to apply the virtual analog stick's vertical axis to.")]
        private FpsInputAxis m_VerticalAxis = FpsInputAxis.MoveY;
        [SerializeField, Tooltip("A falloff curve for the input strength. The horizontal axis is the normalised distance of the touch from the center of the control. The vertical axis is the output strength.")]
        private AnimationCurve m_InputCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Events")]

        [SerializeField, Tooltip("An event fired when the player first touches this control.")]
        private UnityEvent m_OnTouchStarted = null;
        [SerializeField, Tooltip("An event fired when a touch that started on this control is released.")]
        private UnityEvent m_OnTouchEnded = null;

        protected void OnValidate()
        {
            m_InputCurve.preWrapMode = WrapMode.Clamp;
            m_InputCurve.postWrapMode = WrapMode.Clamp;
        }

        public override bool HandleTouch(Touch touch)
        {
            // Get the relative offset from the center
            var rect = rectTransform.rect;
            Vector2 world = rectTransform.TransformPoint(rect.center);
            Vector2 offset = touch.position - world;            
            Vector2 normalised = offset / (rect.width * 0.5f);

            // Apply the input curve
            float magnitude = normalised.magnitude;
            if (magnitude > 0.01f)
            {
                normalised *= m_InputCurve.Evaluate(magnitude) / magnitude;

                // Assign to input axes
                controller.axes[m_HorizontalAxis] += normalised.x;
                controller.axes[m_VerticalAxis] += normalised.y;
            }

            return consume;
        }

        protected override void OnTouchStarted()
        {
            m_OnTouchStarted.Invoke();
        }

        protected override void OnTouchEnded()
        {
            m_OnTouchEnded.Invoke();
        }
    }
}