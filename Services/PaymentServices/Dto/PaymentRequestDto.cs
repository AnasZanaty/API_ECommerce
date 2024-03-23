using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PaymentServices.Dto
{
    public class PaymentRequestDto
    {
        public string CardNumber { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public string Cvc { get; set; }
    }
}
