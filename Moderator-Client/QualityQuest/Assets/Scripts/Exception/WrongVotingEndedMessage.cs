using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongVotingEndedMessage : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WrongVotingEndedMessage"/> class.
    /// </summary>
    public WrongVotingEndedMessage() : base()
    {
        /* FALL THROUGH */
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WrongVotingEndedMessage"/> class with 
    /// a specified error message.
    /// </summary>
    /// 
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public WrongVotingEndedMessage(string message) : base(message)
    {
        /* FALL THROUGH */
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WrongVotingEndedMessage"/> class with 
    /// a specified error message and a reference to the inner exception that is the cause 
    /// of this exception.
    /// </summary>
    /// 
    /// <param name="message"></param>
    /// 
    /// <param name="inner"> The exception that is the cause of the current exception. If the 
    /// innerException parameter is not a null reference, the current exception is raised in 
    /// a catch block that handles the inner exception.
    /// </param>
    public WrongVotingEndedMessage(string message, Exception inner) : base(message, inner)
    {
        /* FALL THROUGH */
    }
}
