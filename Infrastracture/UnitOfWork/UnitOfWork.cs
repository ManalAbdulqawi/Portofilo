﻿using System;
using Core.Interfaces;
using Infrastracture.Repository;

namespace Infrastracture.UnitOfWork
{
    public class UnitOfWork<T>:IUnitOfWork<T>where T:class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Entity
        { get {
                return _entity ??(_entity=new GenericRepository<T>(_context));
            } }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
