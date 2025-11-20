using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Errors";

        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string Field { get; set; } 
        public IEnumerable<string> Errors  { get; set; }
    }
}
