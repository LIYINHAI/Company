using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//四个引用
using Company.IDAL;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Company.DAL
{
   public partial class BaseDAL<T> where T :class,new()
    {
        //对IBaseDAL的具体实现
        private DbContext dbcontext =DbContextFactory.Create();
        //增加
        public void Add(T t)
        {
            dbcontext.Set<T>().Add(t);
        }
        //删除
        public void Delect(T t)
        {
            dbcontext.Set<T>().Remove(t);
        }
        //修改
        public void Update(T t)
        {
            dbcontext.Set<T>().AddOrUpdate(t);
        }
        //页面
        public IQueryable<T> GetModels(Expression<Func<T, bool>> whereLambda)
        {
            return dbcontext.Set<T>().Where(whereLambda);
        }
        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> whereLamdba)
        {
            //是否升序
            if (isAsc)
            {
                return dbcontext.Set<T>
            ().Where(whereLamdba).OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return dbcontext.Set<T>().Where(whereLamdba).OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        public bool SaveChanges()
        {
            return dbcontext.SaveChanges() > 0;
        }
    }
}
