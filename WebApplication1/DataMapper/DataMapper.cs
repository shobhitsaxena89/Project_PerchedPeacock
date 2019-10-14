using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.Models;

namespace DataMapper
{
    public class DataMapper<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        public DataMapper()
        {
            Mapper.CreateMap<VehichleData, VehichleDataDb>();
            Mapper.CreateMap<VehichleDataDb, VehichleData>();
        }
        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }  
}
