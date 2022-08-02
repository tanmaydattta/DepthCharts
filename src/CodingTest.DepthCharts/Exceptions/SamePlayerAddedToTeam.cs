using System;
using System.Runtime.Serialization;

[Serializable]
public class SamePlayerAddedToTeam : Exception
{
    public SamePlayerAddedToTeam()
    {
    }

    public SamePlayerAddedToTeam(string message) : base(message)
    {
    }

    public SamePlayerAddedToTeam(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected SamePlayerAddedToTeam(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}