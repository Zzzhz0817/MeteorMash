using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(string globalsFilePath)
    {
        // compile ink file
        string inkJSON = File.ReadAllText(globalsFilePath);
        Ink.Compiler compiler = new Ink.Compiler(inkJSON);
        Story globalsVariablesStory = compiler.Compile();

        // initialize variables
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalsVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalsVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            // Debug.Log("Variable " + name + " initialized with value " + value);
        }
    }

    public void StartListening(Story story)
    {
        VariableToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }
    
    private void VariableChanged(string variableName, Ink.Runtime.Object value)
    {
        // Debug.Log("Variable " + variableName + " changed to " + value);
        if (variables.ContainsKey(variableName))
        {
            variables.Remove(variableName);
            variables.Add(variableName, value);
        }
    }

    private void VariableToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
