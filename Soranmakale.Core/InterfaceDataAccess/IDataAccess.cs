﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Soranmakale.Core.InterfaceDataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List();

        List<T> List(Expression<Func<T, bool>> kosul);

        IQueryable<T> IQueryableList();

        int Insert(T obj);

        int Save();

        int Delete(T obj);

        T Find(Expression<Func<T, bool>> kosul);

        int Update(T obj);
    }
}
