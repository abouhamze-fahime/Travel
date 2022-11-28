using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Generator
{
  public  class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            //globaly unique identifier
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
