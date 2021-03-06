﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T SelectByID(int id);
        void Create(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
