// Copyright 2016 Florida International University.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Developed by Fernando Mendez (http://github.com/fernmndez) 

using UnityEngine;

namespace Assets.GoogleCloudSpeech.Commands {
    public class ExampleCommand : ICommand {

        /// <summary>
        ///     Called when command handler is being initialized
        /// </summary>
        public void Start() {

        }

        /// <summary>
        ///     The main command handler will call this function when the user says one of the keywords
        /// </summary>
        /// <param name="command">The full command that was heard</param>
        /// <param name="keyword">The keyword that triggered the call</param>
        public void HandleCommand(string command, string keyword) {
            Debug.Log("Example Command Handler is Handling a Commmand");
            Debug.Log("The full command heard was " + command);
            switch (keyword.ToLower()) {
                case "example":
                    HandleExample();
                    break;
                case "test":
                    HandleTest();
                    break;
                default:
                    Debug.LogError("ExampleCommand handler was called with an invalid keyword!");
                    break;
            }
        }

        /// <summary>
        ///     Gets the list of keywords this command handler should handle
        /// </summary>
        /// <returns>Returns string array of keywords this command handler should handle</returns>
        public string[] GetKeywords() {
            return new[] {"Example", "Test"};
        }

        /// <summary>
        ///     Sample function to print out that the example keyword was triggered
        /// </summary>
        private void HandleExample() {
            Debug.Log("We're doing the example.");
        }

        /// <summary>
        ///     Sample function to print out that the test keyword was triggered
        /// </summary>
        private void HandleTest() {
            Debug.Log("We're testing.");
        }
    }
}