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