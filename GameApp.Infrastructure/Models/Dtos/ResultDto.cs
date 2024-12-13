using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Infrastructure.Models.Dtos
{
    public class ResultDto<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int statusCode { get; set; } = 200;

        public static ResultDto<T> Success(T data, string? message = null)
        {
            var result = new ResultDto<T>()
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
            return result;
        }
        public static ResultDto<T> Error(string message, T? data = default)
        {
            var result = new ResultDto<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message
            };
            return result;
        }
        public static ResultDto<T> Error(string message)
        {
            var result = new ResultDto<T>()
            {
                IsSuccess = false,
                Message = message
            };
            return result;
        }
    }
}
