﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastracture.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;

        //this is like a table in repository
        private  DbSet<T> table=null;

        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(object Id)
            {
            T existing = GetById(Id);
            table.Remove(existing);
        }

            public IEnumerable<T> GetAll()
            {
               return table.ToList();
            }

            public T GetById(object Id)
            {
                return table.Find(Id);
            }

            public void Insert(T entity)
            {
                table.Add(entity);
            }

            public void Update(T entity)
            {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            }
        }
    }

