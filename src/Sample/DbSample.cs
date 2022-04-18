using System.Diagnostics;
using LinqToDB;
using LinqToDB.DataProvider;
using Sample.Dto;

namespace Sample;

public class DbSample : LinqToDB.Data.DataConnection
{
    public DbSample(string providerName,
                    string connectionString) : base(providerName, connectionString)
    {
        
        MappingSchema.SetConverter<DateTime, DateTime>(x =>
        {
            var v = x.ToUniversalTime();
            return v;
        });

        
    }

    public ITable<History> History => GetTable<History>();
}
