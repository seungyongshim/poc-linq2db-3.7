using LinqToDB.Mapping;

namespace Sample.Domain;

public enum EventType
{
    [MapValue('R')] Ready,
    [MapValue('S')] Sending,
    [MapValue('T')] Sent,
    [MapValue('E')] Received
}
