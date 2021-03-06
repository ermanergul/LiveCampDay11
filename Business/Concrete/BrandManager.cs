﻿using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _branddal;
        public BrandManager(IBrandDal brandDal)
        {
            _branddal = brandDal;
        }
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            //ValidationTool.Validate(new BrandValidator(), brand);
            _branddal.Add(brand);

            return new SuccessResult(BrandMessages.Added);
        }
        public IResult Delete(Brand brand)
        {
            _branddal.Delete(brand);
            return new SuccessResult(BrandMessages.Deleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_branddal.GetAll(),BrandMessages.Listed);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_branddal.Get(b => b.BrandId == brandId),BrandMessages.Get);
        }
        
        public IResult Update (Brand brand)
        {
            //ValidationTool.Validate(new BrandValidator(), brand);
            _branddal.Update(brand);
            return new SuccessResult(BrandMessages.Update);
        }
    }
}
