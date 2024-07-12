﻿using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request) 
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var trip = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            
            dbContext.Trips.Add(trip);
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = trip.EndDate,
                StartDate = trip.StartDate,
                Name = trip.Name,
                Id = trip.Id
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errorMessagens = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessagens);
            }
        }
    }
}
