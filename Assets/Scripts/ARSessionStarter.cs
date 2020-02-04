using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts
{
    public class ARSessionStarter : MonoBehaviour
    {
        private ARSessionStarterView _view;
        private ARSession _session;

        private void Awake()
        {
            _view = FindObjectOfType<ARSessionStarterView>();
            if (_view == null)
            {
                Debug.LogWarning("No ARSessionMonitorView was found in the scene");
            }
            _session = FindObjectOfType<ARSession>();
            if (_session == null)
            {
                Debug.LogWarning("There is no ARSession in the scene for the ARSessionMonitor to monitor");
            }
            else
            {
                ARSession.stateChanged += ARSession_stateChanged;
                StartCoroutine(StartSession());
            }
        }

        private void ARSession_stateChanged(ARSessionStateChangedEventArgs obj)
        {
            if(_view != null)
            {
                _view.SessionState = ARSession.state.ToString();
            }
        }

        private IEnumerator StartSession()
        {
            if ((ARSession.state == ARSessionState.None 
                || ARSession.state == ARSessionState.CheckingAvailability))
            {
                yield return ARSession.CheckAvailability();
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                Debug.LogWarning("Device does not support AR");
            }
            else
            {
                // Start the AR session
                _session.enabled = true;
            }
        }
    }
}
