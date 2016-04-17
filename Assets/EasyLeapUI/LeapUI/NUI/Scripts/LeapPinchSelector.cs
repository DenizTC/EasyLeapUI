using UnityEngine;

namespace Leap.PinchUtility
{

    public class LeapPinchSelector : LeapPinchDetector
    {

        [SerializeField]
        private int _layerMask;

        private PinchSelectable _lastPinched;

        private bool _lastPinchHit = false;

        protected override void Update()
        {
            base.Update();

            if (_isPinching)
            {
         
                if (_didChange)
                {

                    var screenPoint = Camera.main.WorldToScreenPoint(_pinchPos);
                    var ray = Camera.main.ScreenPointToRay(screenPoint);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10, _layerMask))
                    {
                        _lastPinchHit = true;
                        _lastPinched = hit.transform.GetComponent<PinchSelectable>();
                        _lastPinched.OnPinch(this);
                    }
                    else
                    {
                        _lastPinchHit = false;
                    }

                }// changed from !pinching to pinching
            }// isPinching
            else
            {
                if (_didChange && _lastPinchHit) {
                    _lastPinched.OnPinchRelease();
                }// changed from pinching to !pinching
                _lastPinchHit = false;
            }// not pinching

        }// update

    }

}