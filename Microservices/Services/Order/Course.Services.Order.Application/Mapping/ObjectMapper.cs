﻿using AutoMapper;
using System;

namespace Course.Services.Order.Application.Mapping
{

    //Application layer dont have a startup.cs and we cant use DI so we using this instead of DI
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
