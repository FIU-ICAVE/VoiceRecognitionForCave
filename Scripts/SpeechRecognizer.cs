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

using System;
using GoogleCloudSpeech;
using GoogleCloudSpeech.Types;
using UnityEngine;

namespace Assets.GoogleCloudSpeech.Scripts {
    public class SpeechRecognizer : MonoBehaviour {
        private CloudSpeechClient _speechClient;

        private readonly int _inputSampleRate = 16000;
        private readonly string _inputAudioEncoding = "LINEAR16";
        private CommandDispatcher _commandDispatcher;
        private RecognitionStatus _recognitionStatus;

        [Header("Cloud Speech API Configuration")]
        public string GoogleSpeechApiKey = "YOUR_API_KEY";

        [Tooltip("https://www.rfc-editor.org/rfc/bcp/bcp47.txt")]
        public string BCP27InputLanguageCode = "en-US";

        [Tooltip("Maximum Number of possible alternative voice interpretations suggested")]
        public int MaxAlternativeRecognition = 1;

        [Tooltip("Should the voice recognition service try to filter out profanities. Ex. F**k")]
        public bool ProfanityFilter;

        [Tooltip("Name of button you want recognition to begin on")]
        public string ButtonName = "Jump";

        [Tooltip("Should a textmesh be placed on camera to give user status information")]
        public bool RecognitionStatus = true;

        private void Start() {
            if (!GoogleSpeechApiKey.Equals(string.Empty) || GoogleSpeechApiKey.Equals("YOUR_API_KEY")) {
                _commandDispatcher = gameObject.GetComponentInChildren<CommandDispatcher>();
                _recognitionStatus = gameObject.AddComponent<RecognitionStatus>();
                _recognitionStatus.enabled = RecognitionStatus;
                _speechClient = new CloudSpeechClient(GetSpeecConfiguration(), gameObject);
                _speechClient.Initialize(GoogleSpeechApiKey);
            } else {
                Debug.LogError("API Key must be set!");
                Application.Quit();
                
            }
        }

        
        private void Update() {
            if (_speechClient.HasNewResponse()) {
                if (RecognitionStatus) {
                    _recognitionStatus.FinishedProcessing();
                }
                Debug.Log("Handling new command");
                _commandDispatcher.HandleCommand(_speechClient.GetResponse());
            }
            _speechClient.Update();
            if (getReal3D.Input.GetButtonDown(ButtonName)) {
                if (RecognitionStatus) {
                    _recognitionStatus.Recording();
                }
                    
                _speechClient.BeginRecognizing();
            } else if (getReal3D.Input.GetButtonUp(ButtonName)) {
                if (RecognitionStatus) {
                    _recognitionStatus.ProcessingAudio();
                }
                _speechClient.FinishRecognizing();
            }
        }

        private RecognitionConfig GetSpeecConfiguration() {
            return new RecognitionConfig
            {
                Encoding = _inputAudioEncoding,
                SampleRate = _inputSampleRate,
                LanguageCode = BCP27InputLanguageCode,
                MaxAlternatives = MaxAlternativeRecognition,
                ProfanityFilter = ProfanityFilter
            };
        }
    }

}