/*
Team is prepared to play for a League. 
has number of players arranged in a depthChart 
*/

using System;
using System.Runtime.Serialization;

[Serializable]
public class LeageDoesNotAllowPositionException : Exception
{
    public LeageDoesNotAllowPositionException()
    {
    }

    public LeageDoesNotAllowPositionException(string message) : base(message)
    {
    }

    public LeageDoesNotAllowPositionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected LeageDoesNotAllowPositionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}