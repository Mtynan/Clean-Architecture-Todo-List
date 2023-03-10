using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common
{
    public abstract class Response
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

    public class Response<T> : Response
    {
        public T Value { get; set; }

        public static Response<T> Success(T value) => new Response<T> { IsSuccess = true, Value = value };
        public static Response<T> Failure(string error) => new Response<T> { IsSuccess = false, Error = error };
    }
}