using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
namespace EastlawUI_v2
{
    public class IPIntegrationValidation
    {
        EastLawBL.Users obju = new EastLawBL.Users();
        // true if ipAddress falls inside the CIDR range, example
        // bool result = IsInRange("10.50.30.7", "10.0.0.0/8");
        private bool IsInRange(string ipAddress, string CIDRmask)
        {
            string[] parts = CIDRmask.Split('/');

            int IP_addr = BitConverter.ToInt32(IPAddress.Parse(parts[0]).GetAddressBytes(), 0);
            int CIDR_addr = BitConverter.ToInt32(IPAddress.Parse(ipAddress).GetAddressBytes(), 0);
            int CIDR_mask = IPAddress.HostToNetworkOrder(-1 << (32 - int.Parse(parts[1])));

            return ((IP_addr & CIDR_mask) == (CIDR_addr & CIDR_mask));
        }

        public int CheckIPPool(string ip)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = obju.GetActiveIPPool();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        if (IsInRange(ip, dt.Rows[a]["IPRange"].ToString()))
                        {
                            return int.Parse(dt.Rows[a]["UserID"].ToString());
                        }
                    }
                    return 0;
                }
                return 0;
            }
            catch(Exception ex) {
                return 0;
            }
        }
    }
}