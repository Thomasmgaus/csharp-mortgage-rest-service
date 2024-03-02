namespace mortgage_application.Model
{
    public class MortgageDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MortgageCollectionName { get; set; } = null!;
    }
}
