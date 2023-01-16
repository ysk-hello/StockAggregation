using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ComObjSample
{
    // ★publicにすること

    [Guid("1DF00733-7D51-4C1D-9419-F8CCD5B2638A")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ISample
    {
        [Description("test")]
        string SayHello();

        [Description("会社名を取得する。")]
        string GetCompanyName(string code);
    }
}
