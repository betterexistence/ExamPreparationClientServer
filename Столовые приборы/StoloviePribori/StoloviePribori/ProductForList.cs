using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StoloviePribori
{
    public class ProductForList
    {
        public Brush Background { get; set; }
        public ImageSource ProductPhoto { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductManufacturer { get; set; }
        public string ProductCost { get; set; }
        public string ProductQuantityInStock { get; set; }
    }
}
