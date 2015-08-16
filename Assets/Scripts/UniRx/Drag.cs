using System;
using UnityEngine;
using UniRx;

public class Drag
{
    public static IObservable<long> TouchUpStream(int fingerId)
    {
        return Observable.EveryUpdate()
        .Where(_ =>
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == fingerId)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            return true;
                        default:
                            return false;
                    }
                }
            }

            return true;
        });
    }
}
