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
using System.Collections.Generic;
using System.Reflection;
using Assets.GoogleCloudSpeech.Commands;
using GoogleCloudSpeech.Types;
using UnityEngine;

public class CommandDispatcher : getReal3D.MonoBehaviourWithRpc {
    private List<string> _commandClasses;
    private List<ICommand> _commandInstances;
    [Range(0.0f, 1.0f)]
    public float MinimumCommandConfidence = 0.4f;

    public bool PrintRawTranscripts;

    private void Start() {

        var AssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        _commandClasses = new List<string>();
        _commandInstances = new List<ICommand>();
        foreach (var assemblyType in AssemblyTypes) {
            if (assemblyType.IsClass && !assemblyType.IsAbstract) {
                if ((assemblyType.Namespace != null) &&
                    assemblyType.Namespace.Equals("Assets.GoogleCloudSpeech.Commands")) {
                    _commandClasses.Add(assemblyType.FullName);
                }
            }
        }
        foreach (var commandClass in _commandClasses)
        {
            var commandType = Type.GetType(commandClass);
            if (commandType != null) {
                var instance = (ICommand)Activator.CreateInstance(commandType);
                instance.Start();
                _commandInstances.Add(instance);
            }
        }
    }
    [getReal3D.RPC]
    private void ExecuteCommand(string command)
    {
        foreach (var commandInstance in _commandInstances)
        {
            foreach (var keyword in commandInstance.GetKeywords())
            {
                if (command.Contains(keyword.ToLower())) {
                    commandInstance.HandleCommand(command, keyword);
                }
            }

        }

    }

    public void HandleCommand(Response getResponse) {
        try {
            foreach (var result in getResponse.results) {
                foreach (var alternative in result.alternatives) {
                    if (PrintRawTranscripts) {
                        Debug.Log(alternative.confidence + "\t" + alternative.transcript);
                    }
                    if (alternative.confidence > MinimumCommandConfidence) {
                        getReal3D.RpcManager.call("ExecuteCommand", alternative.transcript.ToLower());
                    } else {
                        gameObject.AddComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("unable_to_understand"));
                        Debug.LogWarning("Confidence of response was " +  alternative.confidence + " which is lower than the minimum command confidence\nIf this issue persists you might want to try raising the minimum command confidence.");
                    }
                }
            }
        } catch (Exception) {
            gameObject.AddComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("unable_to_understand"));
        }

    }


}