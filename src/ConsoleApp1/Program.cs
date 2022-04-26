using LinqToDB;
using LinqToDB.Data;
using Sample;
using Sample.Domain;
using Sample.Dto;

DataConnection.TurnTraceSwitchOn();
DataConnection.WriteTraceLine = (m, d, l) => Console.WriteLine($"[{l}]{m} {d}");

using var db = new DbSample(ProviderName.MySql,
            "Server=127.0.0.1;Database=Sample1234;Uid=root;Pwd=root;");

var sp = db.DataProvider.GetSchemaProvider();
var dbSchema = sp.GetSchema(db);
if (!dbSchema.Tables.Any(t => t.TableName == "History"))
{
    db.CreateTable<History>();
}

await db.History.InsertWithInt64IdentityAsync(() => new History
{
    DateTime = DateTime.UtcNow,
    EventType = EventType.Sent,
    TraceId = "111"
});

var q = from x in db.History
        where x.Id == 6
        select new EventSendResult(x.DateTime, x.EventType, new(x.TraceId));

foreach (var x in q)
{
    Console.WriteLine(x);
}
