using UnityEngine;

namespace Assets.GoogleCloudSpeech.Scripts {
    public class RecognitionStatus : MonoBehaviour {
        private Camera _camera;
        private bool _isDisplaying;
        private TextMesh _statusTextMesh;
        private float _timeDisplayed;
        // Use this for initialization
        private void Start() {
            _camera = Camera.main;
            _statusTextMesh = _camera.gameObject.AddComponent<TextMesh>();
            _statusTextMesh.offsetZ = 30.0f;
            
            _statusTextMesh.text = "";
        }

        private void Update() {
            if (_isDisplaying) {
                _timeDisplayed += Time.deltaTime;
            }
            if (_timeDisplayed >= 5) {
                _timeDisplayed = 0f;
                _isDisplaying = false;
                _statusTextMesh.text = "";
            }

        }

        public void Recording() {
            _timeDisplayed = 0f;
            _isDisplaying = true;
            _statusTextMesh.text = "Recording...";
        }

        public void ProcessingAudio() {
            _timeDisplayed = 0f;
            _isDisplaying = true;
            _statusTextMesh.text = "Processing audio...";
        }

        public void FinishedProcessing() {
            _timeDisplayed = 0f;
            _isDisplaying = true;
            _statusTextMesh.text = "Processing finished...";
        }

        
    }
}