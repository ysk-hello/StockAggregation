using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComObjSample
{
    // HKEY_CLASSES_ROOT\ComObjSample.Sample

    [Guid("52B7B71B-BC5F-4545-BBD5-21E3E1A48696")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(ISample))]
    public class Sample : ISample
    {
        public string SayHello()
        {
            return "Hello, world.";
        }

        public string GetCompanyName(string code)
        {
            var company = new Company()
            {
                Country = Country.JAPAN,
                Code = code
            };
            var kabutan = new KabutanAccess(company);

            return kabutan.GetStockDetailData()?.Name ?? String.Empty;
        }
    }
}
