using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.GoogleCloudSpeech.Commands;
using GoogleCloudSpeech.Types;
using UnityEngine;

public class CommandHandler : getReal3D.MonoBehaviourWithRpc {
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
                    }
                }
            }
        } catch (Exception) {
            gameObject.AddComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("unable_to_understand"));
        }

    }


}