namespace JobOffersPortal.Contracts.Contracts.Queries
{
    public class GetAllByFilterQuery : IFilterQuery
    {
        public string UserId { get; set; }
    }
}
