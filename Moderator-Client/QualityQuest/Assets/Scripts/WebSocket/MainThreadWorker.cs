using System;
using System.Collections.Concurrent;
using UnityEngine;

/// <summary>
/// class to solve the threading problem in Unity,
/// because the WebSocket does not work on the same thread as Unity and this problem leads to errors.
/// </summary>
public class MainThreadWorker : MonoBehaviour
{
    // added actions will be executed in the main thread
    ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

    /// <summary>
    /// The unity update method is called every frame. The method processes all passed actions in the queue.
    /// </summary>
    private void Update()
    {
        // execute all actions added since the last frame
        while (actions.TryDequeue(out var action))
        {
            action?.Invoke();
        }
    }

    /// <summary>
    /// Method to add another action to the queue.
    /// </summary>
    /// <param name="action">The action you want to add to the queue.</param>
    public void AddAction(Action action)
    {
        if (action != null) actions.Enqueue(action);
    }
}