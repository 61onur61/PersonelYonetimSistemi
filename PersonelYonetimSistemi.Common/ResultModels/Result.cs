namespace PersonelYonetimSistemi.Common.ResultModels
{
    public class Result<T> : IResult
    {
        public bool BasariliMi { get; set; }
        public string Mesaj { get; set; }

        public T Veri { get; set; }

        public int ToplamSayi { get; set; }

        public Result(bool basariliMi, string mesaj)
            : this(basariliMi,mesaj, default(T))
        {

        }

        public Result(bool basariliMi, string mesaj,T veri)
            : this(basariliMi, mesaj, veri, 0)
        {

        }

        public Result(bool basariliMi, string mesaj, T veri, int toplamSayi)
        {
            BasariliMi = basariliMi;
            Mesaj = mesaj;
            Veri = veri;
            ToplamSayi = toplamSayi;
        }
    }
}
