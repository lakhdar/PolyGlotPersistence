namespace Infrastructure.Data.Core
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    internal class InMemoryDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
    {
        private readonly IEnumerator<T> _inner;

        public T Current
        {
            get
            {
                return this._inner.Current;
            }
        }

        object IDbAsyncEnumerator.Current
        {
            get
            {
                return (object)this.Current;
            }
        }

        public InMemoryDbAsyncEnumerator(IEnumerator<T> inner)
        {
            this._inner = inner;
        }

        public void Dispose()
        {
            this._inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<bool>(this._inner.MoveNext());
        }
    }
}
