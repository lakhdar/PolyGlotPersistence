// Decompiled with JetBrains decompiler
// Type: Pedago.Infrastructure.Data.Core.InMemoryDbAsyncEnumerable`1
// Assembly: Infrastructure.Data.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 63C97C68-8138-419C-94E3-46CB97551F41
// Assembly location: C:\Pedago\Solution1\ASP.NET.MVC5.Client\bin\Infrastructure.Data.Core.dll

using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data.Core
{
    public class InMemoryDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IDbAsyncEnumerable, IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable
    {
        IQueryProvider IQueryable.Provider
        {
            get
            {
                return (IQueryProvider)new InMemoryDbAsyncQueryProvider<T>((IQueryProvider)this);
            }
        }

        public InMemoryDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public InMemoryDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return (IDbAsyncEnumerator<T>)new InMemoryDbAsyncEnumerator<T>(Enumerable.AsEnumerable<T>((IEnumerable<T>)this).GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return (IDbAsyncEnumerator)this.GetAsyncEnumerator();
        }
    }
}
