using LinqToDB.Mapping;
using Sample.Domain;

namespace Sample.Dto;


public record History
{
    [PrimaryKey, Identity]
    public long Id { get; init; }

    public DateTime DateTime { get; init; }
    public EventType EventType { get; init; }
    public string TraceId { get; init; }  
}
