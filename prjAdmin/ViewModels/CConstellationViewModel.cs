using prjAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.ViewModels
{
    public class CConstellationViewModel
    {
        private Constellation _con;
        public CConstellationViewModel()
        {
            _con = new Constellation();
        }
        public Constellation constellation
        {
            get { return _con; }
            set { _con = value; }
        }
        public int ConstellationId
        {
            get { return _con.ConstellationId; }
            set { _con.ConstellationId = value; }
        }
        public string ConstellationName
        {
            get { return _con.ConstellationName; }
            set { _con.ConstellationName = value; }
        }
        public int? ConstellationProductId
        {
            get { return _con.ConstellationProductId; }
            set { _con.ConstellationProductId = value; }
        }

    }
}
