﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Bateeq.Service.Sales.Lib.BusinessLogic.Interface
{
    public interface IDeleteable
    {
        Task<int> Delete(int id);
    }
}
