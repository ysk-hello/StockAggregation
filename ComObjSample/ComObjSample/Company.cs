using System.ComponentModel;

namespace ComObjSample
{
    public enum Country
    {
        JAPAN = 0,
        USA = 1,
    }

    public class Company
    {
        public Country Country { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
