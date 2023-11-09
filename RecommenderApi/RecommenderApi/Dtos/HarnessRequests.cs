namespace RecommenderApi.Api.Dtos
{
    /// <summary>
    /// Input for Harness server event endpoint POST /engines/<engine-id>/events
    /// Creates an event but may not report its ID since the Event may not be persisted, only used in the algorithm
    /// </summary>
    public class HarnessInputRequest
    {
        /// <summary>
        /// Name of the event that is defined in the UR engine configuration. The value must be one of the "name"s in
        /// the "indicators" array from the UR engine's JSON config.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// The entity for which the recommendation should be given. This is always "user", do not use any other type for indicators.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// This is always "item", do not use any other type for indicators.
        /// </summary>
        public string TargetEntityType { get; set; }

        /// <summary>
        /// Id/Name of the item that should be given as recommendation.
        /// </summary>
        public string TargetEntityId { get; set; }

        /// <summary>
        /// The ISO8601 formatted string for the time the event occurred. Any datetime should be absolute, with a time zone or in 
        /// UTC format. Basically use a time zone or offset from GMT. The popular "Zulu" encoding for UTC is often used. Any truncated
        /// datetime is not supported since the Harness and the UR can accept events from all over the globe and so the datetimes must
        /// be applicable to anywhere.
        /// </summary>
        public DateTime EventTime { get; set; }
    }

    public class HarnessSetPropertyRequest
    {
        public string Event { get; set; }
        public string EntityType { get; set; }
        public string TargetEntityType { get; set; }
        public Property Properties { get; set; }
        public DateTime EventTime { get; set; }
    }

    public class Property
    {
        public string Category { get; set; }
    }
}
