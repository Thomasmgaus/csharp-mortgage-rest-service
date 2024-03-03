using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mortgage_application.Model
{
    public class Applicant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public List<MonthlyPayment> MonthlyMortgagePayment { get; set; }

        public Applicant(List<MonthlyPayment> list)
        {
            this.MonthlyMortgagePayment = list;
        }

    }

}
