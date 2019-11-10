namespace AviasalesWebAPI.ModelAPI
{
    public class ticketsV2
    {
        //[DataMember(Name = "")]
        public float value { get; set; }
        public int trip_class { get; set; }
        public string show_to_affiliates { get; set; }
        public string return_date { get; set; }
        public string origin { get; set; }
        public int? number_of_changes { get; set; }
        public string gate { get; set; }
        public string found_at { get; set; }
        public int? duration { get; set; }
        public int? distance { get; set; }
        public string destination { get; set; }
        public string depart_date { get; set; }
        public bool actual { get; set; }
    }
}
