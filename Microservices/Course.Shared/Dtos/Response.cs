using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Course.Shared.Dtos
{
    
    public class Response<T>
    {
        public T  Data { get;  set; }

        [JsonIgnore] //Dont show when fetching
        public int StatusCode { get;  set; }
        public bool IsSuccessfull  { get;  set; }       
        public List<string> Errors { get;  set; }       //if it fails


        // Static Factory Methods
        public static Response<T> Success(T data,int statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessfull = true,

            };
        }

        //When there isnt need to return data when the transaction finished (delete,update etc.)
        public static Response<T> Success(int statusCode)
        {
            return new Response<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccessfull = true,

            };
        }

        public static Response<T> Fail(List<string> errors, int statusCode)
        {
            return new Response<T>
            {              
                StatusCode = statusCode,
                IsSuccessfull = false,
                Errors =errors

            };
        }

        //When there isnt need to return data when the transaction finished (delete,update etc.)
        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                IsSuccessfull = false,
                Errors = new List<string>(){ error }
            };
        }

    }
}
