using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Data.Core
{
    public sealed class MemorySet<TEntity> : IDbSet<TEntity>, IQueryable<TEntity>, IQueryable, IEnumerable<TEntity>, IEnumerable, IDbAsyncEnumerable<TEntity>, IDbAsyncEnumerable where TEntity : class
    {
        private int _identity = 1;
        private readonly HashSet<TEntity> _data;
        private readonly IQueryable _query;
        private List<PropertyInfo> _keyProperties;

        public ObservableCollection<TEntity> Local
        {
            get
            {
                return new ObservableCollection<TEntity>((IEnumerable<TEntity>)this._data);
            }
        }

        public Type ElementType
        {
            get
            {
                return this._query.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this._query.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return (IQueryProvider)new InMemoryDbAsyncQueryProvider<TEntity>(this._query.Provider);
            }
        }

        public MemorySet(IEnumerable<TEntity> startData = null)
        {
            this.GetKeyProperties();
            this._data = startData != null ? new HashSet<TEntity>(startData) : new HashSet<TEntity>();
            this._query = this._data.AsQueryable();
        }

        private void GetKeyProperties()
        {
            this._keyProperties = new List<PropertyInfo>();
            foreach (PropertyInfo propertyInfo in typeof(TEntity).GetProperties())
            {
                foreach (Attribute attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is KeyAttribute)
                        this._keyProperties.Add(propertyInfo);
                }
            }
        }

        private void GenerateId(TEntity entity)
        {
            if (this._keyProperties.Count != 1 || !(this._keyProperties[0].PropertyType == typeof(int)))
                return;
            this._keyProperties[0].SetValue((object)entity, (object)this._identity++, (object[])null);
        }

        private void Validate(TEntity entity)
        {
            List<ValidationResult> list = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext((object)entity, (IServiceProvider)null, (IDictionary<object, object>)null);
            if (!Validator.TryValidateObject((object)entity, validationContext, (ICollection<ValidationResult>)list))
                throw new Exception(Enumerable.Aggregate<ValidationResult, string>((IEnumerable<ValidationResult>)list, "", (Func<string, ValidationResult, string>)((x, c) => x + "  " + c.ErrorMessage)));
        }

        public TEntity Add(TEntity entity)
        {
            this.GenerateId(entity);
            this.Validate(entity);
            this._data.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            this._data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            if (keyValues.Length != this._keyProperties.Count)
                throw new ArgumentException("Incorrect number of keys passed to find method");

            IQueryable<TEntity> source = this.AsQueryable<TEntity>();
            for (int index = 0; index < keyValues.Length; ++index)
            {
                int x = index;
                source = source.Where(entity =>this._keyProperties[x].GetValue(entity, null).Equals(keyValues[x]));
            }

            return source.SingleOrDefault();
        }

        public TEntity Remove(TEntity entity)
        {
            this._data.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return (IEnumerator<TEntity>)this._data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this._data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            return (IDbAsyncEnumerator<TEntity>)new InMemoryDbAsyncEnumerator<TEntity>((IEnumerator<TEntity>)this._data.GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return (IDbAsyncEnumerator)new InMemoryDbAsyncEnumerator<TEntity>((IEnumerator<TEntity>)this._data.GetEnumerator());
        }
    }
}
