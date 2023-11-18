using System.Text.Json.Serialization;

namespace RecommenderApi.Dtos
{
    public class UrInputDto
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("sex")]
        public string? Sex { get; set; }

        [JsonPropertyName("birthdate")]
        public string? BirthDate { get; set; }

        [JsonPropertyName("education")]
        public string? Education { get; set; }

        [JsonPropertyName("personality")]
        public string? PersonalityType { get; set; }

        [JsonPropertyName("decision_making")]
        public string? DecisionMaking { get; set; }

        [JsonPropertyName("information_retrieval")]
        public string? InformationRetrieval { get; set; }

        [JsonPropertyName("taste")]
        public string? Taste { get; set; }

        [JsonPropertyName("hot_drink")]
        public string? HotDrink { get; set; }

        [JsonPropertyName("alcohol_preference")]
        public string? AlcoholPreference { get; set; }

        [JsonPropertyName("choc_or_vanil")]
        public string? ChocolateOrVanilla { get; set; }

        [JsonPropertyName("coke_or_pepsi")]
        public string? CokeOrPepsi { get; set; }

        [JsonPropertyName("jewel")]
        public string? Jewel { get; set; }

        [JsonPropertyName("black_or_white")]
        public string? BlackOrWhite { get; set; }

        [JsonPropertyName("blue_or_green")]
        public string? BlueOrGreen { get; set; }

        [JsonPropertyName("pink_or_purple")]
        public string? PinkOrPurple { get; set; }

        [JsonPropertyName("fav_car_brand")]
        public string? FavouriteCarBrand { get; set; }

        [JsonPropertyName("fav_season")]
        public string? FavouriteSeason { get; set; }

        [JsonPropertyName("cold_or_hot")]
        public string? ColdOrHot { get; set; }

        [JsonPropertyName("run_or_cycle")]
        public string? RunningOrCycling { get; set; }

        [JsonPropertyName("bus_or_train")]
        public string? BusOrTrain { get; set; }

        [JsonPropertyName("moz_or_beth")]
        public string? MozartOrBethoven { get; set; }

        [JsonPropertyName("cat_or_dog")]
        public string? CatOrDog { get; set; }

        [JsonPropertyName("active_or_relax")]
        public string? ActiveOrRelax { get; set; }

        [JsonPropertyName("hike_or_sightsee")]
        public string? HikingOrSightseeing { get; set; }

        [JsonPropertyName("city_or_village")]
        public string? CityOrVillage { get; set; }

        [JsonPropertyName("plain_or_mountains")]
        public string? PlainOrMountains { get; set; }

        [JsonPropertyName("theatre_or_cinema")]
        public string? TheatreOrCinema { get; set; }

        [JsonPropertyName("book_or_movie")]
        public string? BookOrMovie { get; set; }

        [JsonPropertyName("photo_or_paint")]
        public string? PhotoOrPainting { get; set; }

        [JsonPropertyName("t_or_shirt")]
        public string? ShirtOrTShirt { get; set; }

        [JsonPropertyName("trad_or_prog")]
        public string? TraditionOrProgression { get; set; }

        [JsonPropertyName("email_or_phone")]
        public string? EmailOrPhone { get; set; }

        [JsonPropertyName("folklore_or_industry_art")]
        public string? FolkloreOrIndustryArt { get; set; }

    }
}
