using System.ComponentModel;

namespace FluentPOS.Shared.DTOs.Upload
{
    public enum UploadType
    {
        [Description(@"Images\Catalog\Products")]
        Product,

        [Description(@"Images\Catalog\Brands")]
        Brand,

        [Description(@"Images\Catalog\Categories")]
        Category,

        [Description(@"Images\People\Customers")]
        Customer
    }
}