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
    public class KampanyaBs: IKampanyaBS
    {
        private IKampanyaRepository repo;
        public KampanyaBs(IKampanyaRepository _repo)
        {
            repo = _repo;
        }
        public Kampanya Delete(Kampanya entity)
        {
            return repo.Delete(entity);
        }

        public Kampanya Delete(int Id)
        {
            return repo.Delete(Id);
        }

        public Kampanya Get(Expression<Func<Kampanya, bool>> filter, params string[] includelist)
        {
            return repo.Get(filter, includelist);
        }

        public List<Kampanya> GetAll(Expression<Func<Kampanya, bool>> filter, params string[] includelist)
        {
            return repo.GetAll(filter, includelist);
        }

        public Kampanya GetById(int Id, params string[] includelist)
        {
            return repo.GetById(Id, includelist);
        }

        public Kampanya Insert(Kampanya entity)
        {
            return repo.Insert(entity);
        }

        public Kampanya Update(Kampanya entity)
        {
            return repo.Update(entity);
        }
    }
}
