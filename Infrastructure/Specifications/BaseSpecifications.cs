using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    //Specification Pattern -SECOND STEP-
    public class BaseSpecifications<T>:ISpecifications<T> where T:BaseEntity
    {
       
        public Expression<Func<T, bool>> Criteria { get;}

        //list of includes for adding Includes
        public List<Expression<Func<T, object>>> Includes { get;}

        //First constructor takes criteria 

        
        //for Queries with creiteria
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
            Includes = new List<Expression<Func<T, object>>>();

        }



        //for Queries which has no creiteria
        public BaseSpecifications()
        {
            Includes = new List<Expression<Func<T, object>>>();

        }


        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }
        public int Skip  { get; private set; }
        protected void AddInclude (Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);
    
        protected void AddOrderBy (Expression<Func<T, object>> orderBy)
            => OrderBy=orderBy;
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
           => OrderByDescending = orderByDescending;

        protected void ApplyPagination (int skip , int take)
        {
            skip = Skip;
            take = Take;
            IsPaginated = true;
                
        }
    }
}
