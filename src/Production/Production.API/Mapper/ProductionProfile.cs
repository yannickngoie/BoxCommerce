using AutoMapper;
using EventBus.Messages.Events;
using Production.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Production.API.Mapper
{
    public class ProductionProfile:Profile
    {
        
            public ProductionProfile()
            {
                CreateMap<Activity, ProductionEvent>().ReverseMap();
                CreateMap<Activity, CompleteActivity>().ReverseMap(); 

        }
        }
    }

