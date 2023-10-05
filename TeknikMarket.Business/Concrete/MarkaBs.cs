﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeknikMarket.Business.Abstract;
using TeknikMarket.DataAccess.Abstract;
using TeknikMarket.Model.Entity;

namespace TeknikMarket.Business.Concrete
{
    public class MarkaBs:IMarkaBS
    {
        private IMarkaRepository repo;
        public MarkaBs(IMarkaRepository _repo)
        {
            repo = _repo;
        }
        public Marka Delete(Marka entity)
        {
            return repo.Delete(entity);
        }

        public Marka Delete(int Id)
        {
            return repo.Delete(Id);
        }

        public Marka Get(Expression<Func<Marka, bool>> filter, params string[] includelist)
        {
            return repo.Get(filter, includelist);
        }

        public List<Marka> GetAll(Expression<Func<Marka, bool>> filter, params string[] includelist)
        {
            return repo.GetAll(filter, includelist);
        }

        public Marka GetById(int Id, params string[] includelist)
        {
            return repo.GetById(Id, includelist);
        }

        public Marka Insert(Marka entity)
        {
            return repo.Insert(entity);
        }

        public Marka Update(Marka entity)
        {
            return repo.Update(entity);
        }
    }
}
