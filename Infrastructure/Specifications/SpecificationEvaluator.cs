using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Specifications

{
    //specification pattern -Fifth Step-
    public class SpecificationEvaluator <T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery, ISpecifications<T> specifications)
        {
            var query = inputquery;
            if(specifications.Criteria is not null)
                query= query.Where(specifications.Criteria);
            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDescending is not null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            //if (specifications.IsPaginated==true)
            
            //    query = query.Skip(specifications.Skip).Take(specifications.Take);

            

            query = specifications.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;


        }
        
    }
}
