using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class BadRequestEx : Exception
    {
        public string? Details  { get; }
        public BadRequestEx(string message): base(message) { }
        public BadRequestEx(string message , string details) {
        
            Details = details;
        }
    }
}
