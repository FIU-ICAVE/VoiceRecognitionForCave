#region License
// MIT License
//
// Copyright (c) 2016 Florida International University 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// Developed by Fernando Mendez (http://github.com/fernmndez)
#endregion

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