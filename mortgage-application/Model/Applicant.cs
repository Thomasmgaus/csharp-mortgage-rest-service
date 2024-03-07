using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mortgage_application.Model
{
    public class Applicant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public List<Schedule> Schedules { get; set; }

        public Applicant() {
            Schedules = new List<Schedule>();
        }

        public void AddSchedule(Schedule schedule)
        {
            Schedules.Add(schedule);
        }

    }

}
