using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Helpers
{
    public class ApiResponse<T>
    {
        public int HttpCode { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Values { get; set; }
    }
}
