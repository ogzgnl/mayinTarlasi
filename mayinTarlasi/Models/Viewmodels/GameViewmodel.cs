using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mayinTarlasi.Models.Viewmodels
{
    public class GameViewmodel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<int> flagsUsed { get; set; }
        public Nullable<int> Time { get; set; }
        public int Try { get; set; }

    }
}