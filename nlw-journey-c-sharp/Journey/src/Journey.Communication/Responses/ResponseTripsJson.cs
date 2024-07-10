namespace Journey.Communication.Responses;
public class ResponseTripsJson
{
    public IList<ResponseShortTripJson> Trips { get; set; } = new List<ResponseShortTripJson>();
}
