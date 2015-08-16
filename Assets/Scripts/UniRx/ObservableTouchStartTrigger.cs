using System; // require keep for Windows Universal App
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableTouchStartTrigger : ObservableTriggerBase
    {
        Subject<Touch> touchStart;

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        void Update()
        {
            if (touchStart == null) return;
            if (Input.touchCount <= 0) return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;

            Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                touchStart.OnNext(touch);
        }

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        public IObservable<Touch> TouchStartAsObservable()
        {
            return touchStart ?? (touchStart = new Subject<Touch>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (touchStart != null)
            {
                touchStart.OnCompleted();
            }
        }
    }
}
