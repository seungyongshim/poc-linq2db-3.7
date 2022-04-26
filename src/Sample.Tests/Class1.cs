using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Effects.Database;
using LanguageExt.Effects.Traits;
using LinqToDB.Data;
using Xunit;
using static LanguageExt.Prelude;
using static LanguageExt.Effects.Traits.Database<Sample.Tests.EffectSpec.RT>;
using Sample.Dto;
using Sample.Domain;

namespace Sample.Tests;

public record EffectSpec
{

    [Fact]
    public async Task Effect_Test()
    {
        using var cts = new CancellationTokenSource();
        using var db = new DbSample(LinqToDB.ProviderName.MySql,
           "Server=127.0.0.1;Database=Sample1234;Uid=root;Pwd=root;");

        var q = from __ in unitAff
                from _1 in Insert(new History
                {
                    DateTime = DateTime.UtcNow,
                    EventType = EventType.Sent,
                    TraceId = "111"
                })

        var t = new RT(cts, db);


    }

    public EffectSpec()
    {
        DataConnection.TurnTraceSwitchOn();
        DataConnection.WriteTraceLine = (m, d, l) => Debug.WriteLine($"[{l}] {m} {d}");
    }

    public readonly record struct RT(CancellationTokenSource CancellationTokenSource,
                                     DataConnection DataConnection)
        : HasCancel<RT>,
          HasDatabase<RT>
    {
        public RT LocalCancel => this;
        public CancellationToken CancellationToken => CancellationTokenSource.Token;

        public Aff<RT, DatabaseIO> Database => Eff<RT, DatabaseIO>(rt => new DatabaseLive(rt.DataConnection));
    }

}
