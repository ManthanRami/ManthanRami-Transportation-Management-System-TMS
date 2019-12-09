using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    /// <summary>
    /// The Carrier class models the carrier table found within the TMS database.
    ///
    /// It contains a variety of properties to provide programmers an easy to use interface
    /// with the carrier data.
    /// </summary>
    public class Carrier
    {
        private TmsDal dal;
        public Carrier()
        {
            dal = new TmsDal();
        }

        public uint CarrierID { get; set; }
        public string Name { get; set; }
        public City DepotCity { get; set; }

        public int FtlAvailability { get; set; }
        public int LtlAvailability { get; set; }

        private float _ftlRate = float.MinValue;
        public float FTLRate
        {
            get
            {
                if (_ftlRate == float.MinValue)
                {
                    _ftlRate = dal.GetFtlRate(CarrierID);
                }

                return _ftlRate;
            }
            set
            {
                _ftlRate = value;

                dal.SetFtlRate(CarrierID, _ftlRate);
            }
        }

        private float _ltlRate = float.MinValue;
        public float LTLRate
        {
            get
            {
                if (_ltlRate == float.MinValue)
                {
                    _ltlRate = dal.GetLtlRate(CarrierID);
                }

                return _ltlRate;
            }
            set
            {
                _ltlRate = value;

                dal.SetLtlRate(CarrierID, _ltlRate);
            }
        }

        private float _reeferCharge = float.MinValue;

        public float ReeferCharge
        {
            get
            {
                if (_reeferCharge == float.MinValue)
                {
                    _reeferCharge = dal.GetReeferCharge(CarrierID);
                }

                return _reeferCharge;
            }
            set
            {
                _reeferCharge = value;

                dal.SetReeferCharge(CarrierID, _reeferCharge);
            }
        }
    }
}
