using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forensic.Classes
{
    public class Investigator
    {
       public string InvestigatorID { get; set; }
       public string Password { get; set; }
       public string InvestigatorName { get; set; }
       public string InvestigatorSurname { get; set; }
       public string InvestigatorRank { get; set; }
       public double ConfidenceValue { get; set; }
    }
}
