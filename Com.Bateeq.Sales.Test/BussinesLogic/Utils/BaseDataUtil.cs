﻿using Com.Bateeq.Service.Sales.Lib.Utilities.BaseClass;
using Com.Bateeq.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Sales.Test.BussinesLogic.Utils
{
    public abstract class BaseDataUtil<TFacade, TModel>
        where TFacade : IBaseFacade<TModel>
        where TModel : BaseModel
    {
        private TFacade _facade;
        public BaseDataUtil(TFacade facade)
        {
            _facade = facade;
        }

        public virtual Task<TModel> GetNewData()
        {
            return Task.FromResult(Activator.CreateInstance(typeof(TModel)) as TModel);
        }

        public virtual async Task<TModel> GetTestData()
        {
            var data = await GetNewData();
            await _facade.CreateAsync(data);
            return data;
        }
    }
}
