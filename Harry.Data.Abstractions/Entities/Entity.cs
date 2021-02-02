using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Harry.Data.Entities
{
    /// <summary>
    /// 实体抽象基类
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        protected TPrimaryKey _Id;
        public virtual TPrimaryKey Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }


        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }


        //public override bool Equals(object obj)
        //{
        //    if (obj == null || !(obj is Entity<TPrimaryKey>))
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, obj))
        //    {
        //        return true;
        //    }


        //    var other = (Entity<TPrimaryKey>)obj;
        //    if (IsTransient() && other.IsTransient())
        //    {
        //        return false;
        //    }

        //    var typeOfThis = GetType();
        //    var typeOfOther = other.GetType();
        //    if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
        //    {
        //        return false;
        //    }

        //    return Id.Equals(other.Id);
        //}

        //public override int GetHashCode()
        //{
        //    if (Id == null)
        //    {
        //        return 0;
        //    }

        //    return Id.GetHashCode();
        //}

        ///// <inheritdoc/>
        //public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        //{
        //    if (Equals(left, null))
        //    {
        //        return Equals(right, null);
        //    }

        //    return left.Equals(right);
        //}

        ///// <inheritdoc/>
        //public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        //{
        //    return !(left == right);
        //}

        ///// <inheritdoc/>
        //public override string ToString()
        //{
        //    return $"[{GetType().Name} {Id}]";
        //}
    }
}
