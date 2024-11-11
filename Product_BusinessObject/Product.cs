using System;
using System.Xml.Serialization;

namespace NguyenPhuongNam_SE173557
{
    [XmlRoot("Product")]

    public class ContactList
    {
        [XmlElement("Product")]
        public List<Product> Product { get; set; }

    }
    public class Product
    {
        [XmlElement("ProductId")]
        public int ProductId { get; set; }

        [XmlElement("ProductName")]
        public string ProductName { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }

        [XmlElement("Quantity")]
        public int Quantity { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

    }
}
