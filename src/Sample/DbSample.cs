using LinqToDB;
using Sample.Dto;

namespace Sample;

public class DbSample : LinqToDB.Data.DataConnection
{
    public DbSample(string providerName,
                    string connectionString) : base(providerName, connectionString) =>
        MappingSchema.SetConverter<DateTime, DateTime>(x => x.ToUniversalTime());

    public ITable<History> History => GetTable<History>();
}
