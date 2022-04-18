using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using MySql.Data.MySqlClient;
using Sample.Domain;
using Sample.Dto;
using Xunit;

namespace Sample.Tests;

public class PreludeSpec
{
    [Fact]
    public async Task AddSuccess()
    {
        using var db = new DbSample(LinqToDB.ProviderName.MySql,
            "Server=127.0.0.1;Database=Sample1234;Uid=root;Pwd=root;");

        var sp = db.DataProvider.GetSchemaProvider();
        var dbSchema = sp.GetSchema(db);
        if (!dbSchema.Tables.Any(t => t.TableName == "History"))
        {
            //no required table-create it
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
                select x;

        foreach(var x in q)
        {
            Debug.WriteLine(x);
        }
    }
}
