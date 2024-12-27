using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GameApp.Infrastructure.Models.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            LastTransactionDate = DateTime.Now;
        }

        public virtual byte StatusId { get; set; }

        public virtual DateTime LastTransactionDate { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
        public T Clone<T>()
        {
            return (T)MemberwiseClone();
        }
        private IEnumerable<PropertyInfo> GetPrimaryKeyProperties()
        {
            return
                from prop in GetType().GetProperties()
                where prop.GetCustomAttributes(typeof(KeyAttribute), false).Any()
                select prop;
        }
        public object[] GetPrimaryKeyValues()
        {
            var objs = new List<object>();
            using (var enumerator = GetPrimaryKeyProperties().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    objs.Add(RuntimeHelpers.GetObjectValue(current?.GetValue(this, null)));
                }
            }
            return objs.ToArray();
        }
        public T GetId<T>()
        {
            var objs = GetPrimaryKeyValues();
            return (T)Convert.ChangeType(objs[0].ToString(), typeof(T));
        }




    }
}
