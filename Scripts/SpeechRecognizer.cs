#define NO_GET_REAL_3D

using GoogleCloudSpeech;
using GoogleCloudSpeech.Types;
using UnityEngine;

namespace Assets.GoogleCloudSpeech.Scripts {
    public class SpeechRecognizer : MonoBehaviour {
        private CloudSpeechClient _speechClient;

        private readonly int _inputSampleRate = 16000;
        private readonly string _inputAudioEncoding = "LINEAR16";
        private CommandHandler _commandHandler;

        [Header("Cloud Speech API Configuration")]
        public string GoogleSpeechApiKey = "";

        [Tooltip("https://www.rfc-editor.org/rfc/bcp/bcp47.txt")]
        public string BCP27InputLanguageCode = "en-US";

        [Tooltip("Maximum Number of possible alternative voice interpretations suggested")]
        public int MaxAlternativeRecognition = 1;

        [Tooltip("Should the voice recognition service try to filter out profanities. Ex. F**k")]
        public bool ProfanityFilter;

        [Tooltip("Name of button you want recognition to begin on")]
        public string ButtonName = "Jump";

        private void Start() {
            _commandHandler = gameObject.GetComponentInChildren<CommandHandler>();
            _speechClient = new CloudSpeechClient(GetSpeecConfiguration(), gameObject);
            _speechClient.Initialize(GoogleSpeechApiKey);
        }

        
        private void Update() {
            if (_speechClient.HasNewResponse()) {
                Debug.Log("Handling new command");
                _commandHandler.HandleCommand(_speechClient.GetResponse());
            }
            _speechClient.Update();
            if (getReal3D.Input.GetButtonDown(ButtonName)) {
                _speechClient.BeginRecognizing();
            } else if (getReal3D.Input.GetButtonUp(ButtonName)) {
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