using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyLibrary
{
    public class EntityData<T> where T:class
    {
        public virtual int GetCount()
        {
            using (DbContext context = DbContextFactory.Create())
            {
                return context.Set<T>().Count();
            }
        }

        public int GetCount(Expression<Func<T, bool>> predicate)
        {
            using (DbContext context = DbContextFactory.Create())
            {
                return context.Set<T>().Count(predicate);
            }
        }

        public List<R> Select<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null)
        {
            using (DbContext context = DbContextFactory.Create())
            {
                IQueryable<T> query = context.Set<T>();

                if (predicate != null)
                    query = query.Where(predicate);

                return query.Select(selector).ToList();
                //return context.Set<T>().Select(selector).ToList();
            }
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            using (DbContext context = DbContextFactory.Create())
            {
                IQueryable<T> query = context.Set<T>();

                if (predicate != null)
                    query = query.Where(predicate);

                return query.ToList();
            }
        }

        public void Insert(T entity)
        {
            using (DbContext context = DbContextFactory.Create())
            {
                context.Set<T>().Add(entity);
                context.Entry(entity).State = EntityState.Added;

                context.SaveChanges();
            }
        }

        public virtual void Update(T entity)
        {
            //ClearNavigationProperties(entity);

            using (DbContext context = DbContextFactory.Create())
            {
                context.Entry(entity).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (DbContext context = DbContextFactory.Create())
            {
                context.Entry(entity).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        #region ClearNavigationProperties
		/// <summary>
		/// Navigation Property를 null로 설정한다. Navigation Property 와 Foreign Key가 모두 설정되면 [참조 무결성 제약 조건 위반이 발생했습니다. 참조 제약 조건을 정의하는 속성 값이 관계의 주 개체와 종속 개체 간에 일치하지 않습니다.] 예외가 발생함.
		/// </summary>
		/// <param name="entities"></param>
		protected void ClearNavigationProperties(List<T> entities)
		{
			if (entities.Count == 0)
				return;

			var propertyInfoes = GetNavigationProperties(entities[0].GetType());

			foreach (var propertyInfo in propertyInfoes)
				foreach (var entity in entities)
					propertyInfo.SetValue(entity, null, null);
		}

		/// <summary>
		/// Navigation Property를 null로 설정한다. Navigation Property 와 Foreign Key가 모두 설정되면 [참조 무결성 제약 조건 위반이 발생했습니다. 참조 제약 조건을 정의하는 속성 값이 관계의 주 개체와 종속 개체 간에 일치하지 않습니다.] 예외가 발생함.
		/// </summary>
		/// <param name="entity"></param>
		protected void ClearNavigationProperties(T entity)
		{
			var propertyInfoes = GetNavigationProperties(entity.GetType());

			foreach (var propertyInfo in propertyInfoes)
				propertyInfo.SetValue(entity, null, null);
		}

		protected static List<PropertyInfo> GetNavigationProperties(Type type)
		{
            // reflection

			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(x => x.CanRead && x.CanWrite &&
							x.PropertyType.IsClass && x.GetGetMethod().IsVirtual && x.PropertyType.Name != "String")
				.ToList();
		}
		#endregion
    }
}
