namespace OlimpikonokApi.DTOs
{
    public class SportoloDTO
    {
        public int Id { get; set; }
        public string Nev { get; set; } = null!;
        public string SportagNev { get; set; }
        public int Ermek { get; set; }
        public string OrszagNev { get; set; }
    }
}
