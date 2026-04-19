using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers
{
    public class DatatableResponse<T>
    {
        public T? Data { get; set; }
        public int Draw {  get; set; } = 0;
        public int RecordsFiltered { get; set; } = 0;
        public int RecordsTotal { get; set; } = 0;
    }
}
