using System;

namespace GdaxApi.Models
{
    public class ProductPrice
    {
        public string product_id { get; set; }
        public string trade_id { get; set;  }
        public decimal price { get; set;  }
        public decimal size { get; set;  }
        public decimal bid { get; set;  }
        public decimal ask { get; set;  }
        public decimal volume { get; set;  }
        public DateTime time { get; set;  }
    }
}