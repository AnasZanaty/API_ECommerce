using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CasheServices
{
    public interface ICasheServices
    {
        Task setCaheResponseAsync(string cashKey,object response , TimeSpan TimeToLive );
        Task <string> GetCacheResponseAsync(string cashKey);

    }
}
