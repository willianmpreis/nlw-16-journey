using Journey.Communication.Requests;
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
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.NAME_EMPTY);
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
            }

            if(request.EndDate.Date < request.StartDate.Date)
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
            }
        }
    }
}
