using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobLib.Web
{
    public class Result<T>
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<Error> Errors { get; set; }
        public T Object { get; set; }

        public Result()
        {
            this.Id = 0;
            this.Message = string.Empty;
        }

        public static implicit operator Result<T>(T obj)
        {
            var response = new Result<T>();

            if (obj != null)
            {
                response.Success = true;
                response.Message = "";
            }
            else
            {
                response.Success = false;
                response.Message = "Nenhum dado encontrado";
            }

            response.Object = obj;

            return response;
        }


        public static implicit operator Result(Result<T> result)
        {
            return new Result
            {
                Id = result.Id,
                Message = result.Message,
                Success = result.Success,
                Errors = result.Errors,
                Object = result.Object
            };
        }
    }

    public static class ResultExtentions
    {

        public static Result<IEnumerable<T>> CreateResult<T>(this IEnumerable<T> list)
        {
            var response = new Result<IEnumerable<T>>();

            if (list != null && list.Any())
            {
                response.Success = true;
                response.Message = "";
            }
            else
            {
                response.Success = false;
                response.Message = "Nenhum dado encontrado";
            }

            response.Object = list;

            return response;
        }
    }
    public class Result : Result<object>
    {
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
