namespace Data_Entry_Project
{
    public class Airplane
    {
        public int Id { get; set; }
        public string AirplaneSpec { get; set; }
        public string AirplaneModel { get; set; }
        public string AirplaneNumber { get; set; }
        public byte[] AirplaneImage { get; set; }

        public string AirplaneName
        {
            get
            {
                return $"{AirplaneSpec} { AirplaneModel } { AirplaneNumber }";
            }
        }
    }
  
}
