using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specifications
{
    //Specification pattern -FIRST STEP-
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //crieteria is the expression or delegete which is sent to where
        List <Expression<Func<T, object>>> Includes { get; } //Includes is the sum of includes to be added for query

        Expression<Func<T, object>> OrderByDescending { get; }

        Expression<Func<T, object>> OrderBy { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
