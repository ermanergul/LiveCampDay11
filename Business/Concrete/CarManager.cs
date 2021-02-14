﻿using Business.Abstract;
using Business.Constans;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ////İş KODLARI 
        ////ICarDal ile etkileşim oluştur


        ICarDal _carDal;

        public CarManager(ICarDal carDal)//injection   CarManager newlendiğinde 
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            //InMemoryCarDal ınMemoryCarDal = new InMemoryCarDal();   -> Böyle bir kullanım yaparsak , iş kodlarından binlercesi geçebilir. Bir iş sınıfı başka sınıfları newlemez. BU BİR KURALDIR

            var cars = _carDal.GetAll();
            if (cars.Count == 0)
            {
                return new ErrorDataResult<List<Car>>(_carDal.GetAll(),CarMessages.NullData);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),CarMessages.Listed);

        }
        public IResult Add(Car car)
        {
            if(car.Descriptions.Length < 2 || car.DailyPrice < 0)
            {
                return new ErrorResult(CarMessages.DiscriptionInvalid);
            }
            
            _carDal.Add(car);

            return new SuccessResult(CarMessages.Added);
            
            
        }
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(CarMessages.Deleted);
        }


       public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(CarMessages.Update);

        }

        public IDataResult<List<Car>> GetByDailyPrice(int min, int max)
        {
            var result1 = _carDal.GetAll();
            
            List<Car> cars = new List<Car>(); 
            foreach (var car in result1)
            {
                if (car.DailyPrice > min && car.DailyPrice < max)
                {
                    cars.Add(car);
                }
            }
            if (cars.Count == 0)
            {
                return new ErrorDataResult<List<Car>>(CarMessages.NullData);
            }
            
       
            return new SuccessDataResult<List<Car>>(cars);




        }

        public IDataResult<List<CarDetailDto>> GetDetails()
        {

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<Car> GetById(int carId)
        {
            
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarID == carId));
        }

       
    }
}