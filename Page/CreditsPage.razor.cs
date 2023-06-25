using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photolog.Page
{
    public partial class CreditsPage
    {
        private string GetStyleClass() => Done ?  "animate__bounceOutLeft" : "animate__bounceInLeft";
    }
}
